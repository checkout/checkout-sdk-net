using Checkout.Common;
using Checkout.Marketplace.Payout;
using Checkout.Marketplace.Payout.Request;
using Checkout.Marketplace.Payout.Response;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Marketplace
{
    public class MarketplacePayoutSchedulesIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldUpdateAndRetrieveWeeklyPayoutSchedules()
        {
            var scheduleRequest = new UpdateScheduleRequest
            {
                Enabled = true,
                Threshold = 1000,
                Recurrence = new ScheduleFrequencyWeeklyRequest {ByDay = new[] {DaySchedule.Sunday, DaySchedule.Monday}}
            };

            await GetPayoutSchedulesCheckoutApi().MarketplaceClient()
                .UpdatePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq", Currency.USD, scheduleRequest);

            var response = await GetPayoutSchedulesCheckoutApi().MarketplaceClient()
                .RetrievePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq");

            response.ShouldNotBeNull();
            response.Currency.ShouldNotBeEmpty();
            CurrencySchedule currencySchedule = response.Currency[Currency.USD];
            currencySchedule.ShouldNotBeNull();
            currencySchedule.Enabled.ShouldNotBeNull();
            currencySchedule.Threshold.ShouldNotBeNull();
            currencySchedule.Recurrence.ShouldBeOfType(typeof(ScheduleFrequencyWeeklyResponse));
            ((ScheduleFrequencyWeeklyResponse)currencySchedule.Recurrence).ByDay.Count.ShouldBe(2);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldUpdateAndRetrieveDailyPayoutSchedules()
        {
            var scheduleRequest = new UpdateScheduleRequest
            {
                Enabled = true, Threshold = 1000, Recurrence = new ScheduleFrequencyDailyRequest()
            };

            await GetPayoutSchedulesCheckoutApi().MarketplaceClient()
                .UpdatePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq", Currency.USD, scheduleRequest);

            var response = await GetPayoutSchedulesCheckoutApi().MarketplaceClient()
                .RetrievePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq");

            response.ShouldNotBeNull();
            response.Currency.ShouldNotBeEmpty();
            CurrencySchedule currencySchedule = response.Currency[Currency.USD];
            currencySchedule.ShouldNotBeNull();
            currencySchedule.Enabled.ShouldNotBeNull();
            currencySchedule.Threshold.ShouldNotBeNull();
            currencySchedule.Recurrence.ShouldBeOfType(typeof(ScheduleFrequencyDailyResponse));
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldUpdateAndRetrieveMonthlyPayoutSchedules()
        {
            var scheduleRequest = new UpdateScheduleRequest
            {
                Enabled = true,
                Threshold = 1000,
                Recurrence = new ScheduleFrequencyMonthlyRequest {ByMonthDay = new[] {3, 5}}
            };

            await GetPayoutSchedulesCheckoutApi().MarketplaceClient()
                .UpdatePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq", Currency.USD, scheduleRequest);

            var response = await GetPayoutSchedulesCheckoutApi().MarketplaceClient()
                .RetrievePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq");

            response.ShouldNotBeNull();
            response.Currency.ShouldNotBeEmpty();
            CurrencySchedule currencySchedule = response.Currency[Currency.USD];
            currencySchedule.ShouldNotBeNull();
            currencySchedule.Enabled.ShouldNotBeNull();
            currencySchedule.Threshold.ShouldNotBeNull();
            currencySchedule.Recurrence.ShouldBeOfType(typeof(ScheduleFrequencyMonthlyResponse));
            ((ScheduleFrequencyMonthlyResponse)currencySchedule.Recurrence).ByMonthDay.Count.ShouldBe(2);
        }

        private static Four.CheckoutApi GetPayoutSchedulesCheckoutApi()
        {
            return CheckoutSdk.FourSdk().OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_OAUTH_PAYOUT_SCHEDULE_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_OAUTH_PAYOUT_SCHEDULE_CLIENT_SECRET"))
                .Scopes(FourOAuthScope.Marketplace)
                .Build() as Four.CheckoutApi;
        }
    }
}