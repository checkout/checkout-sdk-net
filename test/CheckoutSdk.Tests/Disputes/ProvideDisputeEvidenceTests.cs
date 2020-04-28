using System.IO;
using System.Threading.Tasks;
using Checkout.Disputes;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Disputes
{
    public class ProvideDisputeEvidenceTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public ProvideDisputeEvidenceTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanProvideDisputeEvidence()
        {
            // simulate a chargeback payment
            var chargebackPaymentRequest = TestHelper.CreateChargebackCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(chargebackPaymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.IsPending.ShouldBeFalse();

            // call disputes endpoint every 10s to wait for payment to transition to disputed state
            var getDisputesRequest = new GetDisputesRequest(paymentId: paymentResponse.Payment.Id);
            DisputeSummary dispute = null;
            while (dispute == null)
            {
                await Task.Delay(TestHelper.PaymentDisputedVerificationInterval());
                var getDisputesResponse = await _api.Disputes.GetDisputesAsync(getDisputesRequest: getDisputesRequest);
                var disputes = getDisputesResponse.Data;
                if (disputes.Count == 1) dispute = disputes[0];
            }

            dispute.PaymentId.ShouldBe(paymentResponse.Payment.Id);
            dispute.Status.ShouldBe("evidence_required");

            // upload a file first
            var pathToFile = @"test_file.png";
            var fileInfo = new FileInfo(fileName: pathToFile);
            var uploadFileResponse = await _api.Files.UploadFileAsync(pathToFile: fileInfo.FullName, purpose: "dispute_evidence");

            uploadFileResponse.ShouldNotBeNull();
            uploadFileResponse.Id.ShouldStartWith("file_");

            // use file as evidence in dispute
            var disputeEvidence = new DisputeEvidence()
            {
                {"additional_evidence_file", uploadFileResponse.Id },
                {"additional_evidence_text", "provide dispute evidence test" }
            };
            await _api.Disputes.ProvideDisputeEvidenceAsync(id: dispute.Id, disputeEvidence: disputeEvidence);

            var getDisputeResponse = await _api.Disputes.GetDisputeAsync(id: dispute.Id);

            getDisputeResponse.ShouldNotBeNull();

        }
    }
}
