using Checkout.Payments;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Disputes
{
    public class DisputesIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldQueryDisputes()
        {
            var from = DateTime.UtcNow.Subtract(TimeSpan.FromHours(24));
            var to = DateTime.Now; // local timezone
            var request = new DisputesQueryFilter { Limit = 250, To = to, From = from };

            var response = await DefaultApi.DisputesClient().Query(request);
            response.ShouldNotBeNull();
            response.Limit.ShouldBe(250);
            response.Skip.ShouldNotBeNull();
            response.TotalCount.ShouldNotBeNull();
            response.To.ShouldNotBeNull();
            response.From.ShouldNotBeNull();

            if (response.TotalCount > 0)
            {
                var dispute = response.Data[0];
                var disputeDetails = await DefaultApi.DisputesClient().GetDisputeDetails(dispute.Id);
                disputeDetails.ShouldNotBeNull();
                disputeDetails.Id.ShouldBe(dispute.Id);
                disputeDetails.Category.ShouldBe(dispute.Category);
                disputeDetails.Status.ShouldBe(dispute.Status);
                disputeDetails.Amount.ShouldBe(dispute.Amount);
                disputeDetails.ReasonCode.ShouldBe(dispute.ReasonCode);
                disputeDetails.Payment.ShouldNotBeNull();
                disputeDetails.Payment.Id.ShouldBe(dispute.PaymentId);
            }
        }

        [Fact]
        private async Task ShouldCreateAndRetrieveFile()
        {
            const string filePath = "./Resources/checkout.jpeg";

            var fileResponse = await DefaultApi.DisputesClient().SubmitFile(filePath, "dispute_evidence");
            fileResponse.ShouldNotBeNull();
            fileResponse.Id.ShouldNotBeNullOrEmpty();

            var fileDetails = await DefaultApi.DisputesClient().GetFileDetails(fileResponse.Id);
            fileDetails.ShouldNotBeNull();
            fileDetails.Id.ShouldNotBeNull();
            fileDetails.Filename.ShouldNotBeNull();
            fileDetails.Purpose.ShouldNotBeNull();
            fileDetails.Size.ShouldNotBeNull();
            fileDetails.UploadedOn.ShouldNotBeNull();
        }

        [Fact(Timeout = 180000, Skip = "Due the time to expect the dispute, just run as needed")]
        private async Task ShouldTestFullDisputesWorkflow()
        {
            //Make the payment
            var payment = await MakeCardPayment(true, 1040L);
            payment.ShouldNotBeNull();
            var query = new DisputesQueryFilter()
            {
                PaymentId = payment.Id
            };

            //Query for dispute
            DisputesQueryResponse queryResponse = null;
            while (queryResponse == null || queryResponse.TotalCount == 0)
            {
                await Nap();
                queryResponse = await DefaultApi.DisputesClient().Query(query);
            }

            queryResponse.ShouldNotBeNull();
            queryResponse.Data[0].PaymentId.ShouldBe(payment.Id);

            //Upload dispute evidence
            const string filePath = "./Resources/checkout.jpeg";
            var fileResponse = await DefaultApi.DisputesClient().SubmitFile(filePath, "dispute_evidence");

            //Provide dispute evidence
            var disputeEvidenceRequest = new DisputeEvidenceRequest()
            {
                ProofOfDeliveryOrServiceFile = fileResponse.Id,
                ProofOfDeliveryOrServiceText = "proof of delivery or service text",
                InvoiceOrReceiptFile = fileResponse.Id,
                InvoiceOrReceiptText = "Copy of the invoice",
                CustomerCommunicationFile = fileResponse.Id,
                CustomerCommunicationText = "Copy of an email exchange with the cardholder",
                AdditionalEvidenceFile = fileResponse.Id,
                AdditionalEvidenceText = "Scanned document"
            };
            var disputeId = queryResponse.Data[0].Id;
            await DefaultApi.DisputesClient().PutEvidence(disputeId, disputeEvidenceRequest);

            //Verify the dispute evidence
            var evidence = await DefaultApi.DisputesClient().GetEvidence(disputeId);
            evidence.ShouldNotBeNull();
            evidence.ProofOfDeliveryOrServiceFile.ShouldBe(disputeEvidenceRequest.ProofOfDeliveryOrServiceFile);
            evidence.ProofOfDeliveryOrServiceText.ShouldBe(disputeEvidenceRequest.ProofOfDeliveryOrServiceText);
            evidence.InvoiceOrReceiptFile.ShouldBe(disputeEvidenceRequest.InvoiceOrReceiptFile);
            evidence.InvoiceOrReceiptText.ShouldBe(disputeEvidenceRequest.InvoiceOrReceiptText);
            evidence.CustomerCommunicationFile.ShouldBe(disputeEvidenceRequest.CustomerCommunicationFile);
            evidence.CustomerCommunicationText.ShouldBe(disputeEvidenceRequest.CustomerCommunicationText);
            evidence.AdditionalEvidenceFile.ShouldBe(disputeEvidenceRequest.AdditionalEvidenceFile);
            evidence.AdditionalEvidenceText.ShouldBe(disputeEvidenceRequest.AdditionalEvidenceText);

            //Submit the dispute
            await DefaultApi.DisputesClient().SubmitEvidence(disputeId);
        }
    }
}