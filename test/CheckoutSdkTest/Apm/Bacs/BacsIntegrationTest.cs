using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Apm.Bacs
{
    public class BacsIntegrationTest : SandboxTestFixture
    {
        public BacsIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "Requires a merchant enabled for Bacs Direct Debit and an existing Bacs instrument")]
        private async Task ShouldSendNotification()
        {
            var request = new BacsNotificationRequest
            {
                SourceId = "src_wmlfc3zyhqzehihu7giusaaawu",
                NotificationType = BacsNotificationType.AdvanceNotice,
                CollectionDate = "2026-07-15",
                Amount = 4999L,
                Currency = Currency.GBP,
                Reference = "INV-12345",
                CustomerEmail = "customer@example.com",
                BillingDescriptor = "CHECKOUT",
                SupportEmail = "support@test.com",
                SupportPhone = "+447700900123"
            };

            var response = await DefaultApi.BacsClient().SendNotification(request);

            response.ShouldNotBeNull();
            response.EventId.ShouldNotBeNullOrEmpty();
        }
    }
}
