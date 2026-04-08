using Checkout.ComplianceRequests.Requests;
using Checkout.ComplianceRequests.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.ComplianceRequests
{
    public class ComplianceRequestsIntegrationTest : SandboxTestFixture
    {
        public ComplianceRequestsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "Requires a payment ID associated with an active compliance request")]
        public async Task GetComplianceRequest_ShouldReturnDetails()
        {
            const string paymentId = "pay_fun26akvvjjerahhctaq2uzhu4";

            var response = await DefaultApi.ComplianceRequestsClient().GetComplianceRequest(paymentId);

            response.ShouldNotBeNull();
            response.PaymentId.ShouldBe(paymentId);
            response.Status.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "Requires a payment ID associated with an active compliance request")]
        public async Task RespondToComplianceRequest_ShouldSucceed()
        {
            const string paymentId = "pay_fun26akvvjjerahhctaq2uzhu4";
            var request = new ComplianceRequestRespondRequest
            {
                Fields = new ComplianceRespondedFields
                {
                    Sender = new List<ComplianceRespondedField>
                    {
                        new ComplianceRespondedField { Name = "date_of_birth", Value = "2000-01-01", NotAvailable = false }
                    }
                },
                Comments = "Providing the requested compliance information"
            };

            var response = await DefaultApi.ComplianceRequestsClient().RespondToComplianceRequest(paymentId, request);

            response.ShouldNotBeNull();
        }
    }
}
