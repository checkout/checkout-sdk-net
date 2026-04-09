using Checkout.HandlePaymentsAndPayouts.GooglePay.Requests;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    public class GooglePayIntegrationTest : SandboxTestFixture
    {
        public GooglePayIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "Requires a valid entity with Google Pay enrollment permissions")]
        public async Task CreateEnrollment_ShouldSucceed()
        {
            var request = new GooglePayEnrollmentRequest
            {
                EntityId = "ent_uzm3uxtssvmuxnyrfdffcyjxeu",
                EmailAddress = "test@example.com",
                AcceptTermsOfService = true
            };

            var response = await DefaultApi.GooglePayClient().CreateEnrollment(request);

            response.ShouldNotBeNull();
        }

        [Fact(Skip = "Requires an actively enrolled Google Pay entity")]
        public async Task RegisterDomain_ShouldSucceed()
        {
            const string entityId = "ent_uzm3uxtssvmuxnyrfdffcyjxeu";
            var request = new GooglePayRegisterDomainRequest
            {
                WebDomain = "checkout-test-domain.com"
            };

            var response = await DefaultApi.GooglePayClient().RegisterDomain(entityId, request);

            response.ShouldNotBeNull();
        }

        [Fact(Skip = "Requires an actively enrolled Google Pay entity")]
        public async Task GetDomains_ShouldReturnDomainList()
        {
            const string entityId = "ent_uzm3uxtssvmuxnyrfdffcyjxeu";

            var response = await DefaultApi.GooglePayClient().GetDomains(entityId);

            response.ShouldNotBeNull();
            response.Domains.ShouldNotBeNull();
        }

        [Fact(Skip = "Requires an actively enrolled Google Pay entity")]
        public async Task GetEnrollmentState_ShouldReturnState()
        {
            const string entityId = "ent_uzm3uxtssvmuxnyrfdffcyjxeu";

            var response = await DefaultApi.GooglePayClient().GetEnrollmentState(entityId);

            response.ShouldNotBeNull();
        }
    }
}
