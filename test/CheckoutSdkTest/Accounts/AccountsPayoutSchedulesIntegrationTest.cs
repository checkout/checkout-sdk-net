using Checkout.Accounts.Payout;
using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Accounts
{
    public class AccountsPayoutSchedulesIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldUpdateAndRetrieveWeeklyPayoutSchedules()
        {
            var scheduleRequest = new UpdateScheduleRequest
            {
                Enabled = true,
                Threshold = 1000,
                Recurrence =
                    new ScheduleFrequencyWeeklyRequest {ByDay = new[] {DaySchedule.Sunday, DaySchedule.Monday}}
            };

            var emptyResponse = await GetPayoutSchedulesCheckoutApi().AccountsClient()
                .UpdatePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq", Currency.USD, scheduleRequest);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            var response = await GetPayoutSchedulesCheckoutApi().AccountsClient()
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

            var emptyResponse = await GetPayoutSchedulesCheckoutApi().AccountsClient()
                .UpdatePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq", Currency.USD, scheduleRequest);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            var response = await GetPayoutSchedulesCheckoutApi().AccountsClient()
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

            var emptyResponse = await GetPayoutSchedulesCheckoutApi().AccountsClient()
                .UpdatePayoutSchedule("ent_sdioy6bajpzxyl3utftdp7legq", Currency.USD, scheduleRequest);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            var response = await GetPayoutSchedulesCheckoutApi().AccountsClient()
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

        private static ICheckoutApi GetPayoutSchedulesCheckoutApi()
        {
            var logFactory = Checkout.TestLoggerFactoryHelper.Create();
            
            return CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_PAYOUT_SCHEDULE_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_PAYOUT_SCHEDULE_CLIENT_SECRET"))
                .Scopes(OAuthScope.Marketplace)
                .LogProvider(logFactory)
                .Build() as CheckoutApi;
        }
    }
}