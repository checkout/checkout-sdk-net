using Checkout.Payments;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Disputes
{
    public class DisputesIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldQueryDisputes()
        {
            var request = new DisputesQueryFilter();

            var response = await DefaultApi.DisputesClient().Query(request);
            response.ShouldNotBeNull();
            response.Limit.ShouldNotBeNull();
            response.Skip.ShouldNotBeNull();
            response.TotalCount.ShouldNotBeNull();
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

        [Fact]
        private async Task ShouldGetDisputeSchemeFiles()
        {
            var query = new DisputesQueryFilter { Limit = 5 };
            var queryResponse = await DefaultApi.DisputesClient().Query(query);

            queryResponse.ShouldNotBeNull();
            if (queryResponse.Data.Count > 0)
            {
                foreach (var dispute in queryResponse.Data)
                {
                    var disputeSchemeFiles = await DefaultApi.DisputesClient().GetDisputeSchemeFiles(dispute.Id);
                    disputeSchemeFiles.ShouldNotBeNull();
                    disputeSchemeFiles.Id.ShouldNotBeNull();
                    disputeSchemeFiles.Files.Count.ShouldBeGreaterThan(0);
                }
            }
        }

        [Fact(Timeout = 180000, Skip = "Due the time to expect the dispute, just run as needed")]
        private async Task ShouldTestFullDisputesWorkflow()
        {
            //Make the payment
            var payment = await MakeCardPayment(true, 1040L);
            payment.ShouldNotBeNull();
            var query = new DisputesQueryFilter {PaymentId = payment.Id};

            //Query for dispute
            DisputesQueryResponse queryResponse = await Retriable(async () =>
                await DefaultApi.DisputesClient().Query(query), HasItems);

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

        private static bool HasItems(DisputesQueryResponse obj)
        {
            return obj.TotalCount > 0;
        }
    }
}