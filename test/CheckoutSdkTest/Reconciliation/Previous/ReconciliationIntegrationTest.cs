using Checkout;
using Checkout.Common;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Reconciliation.Previous
{
    public class ReconciliationIntegrationTest
    {
        private readonly QueryFilterDateRange _queryFilterDateRange = new QueryFilterDateRange
        {
            From = DateTime.Now.Subtract(TimeSpan.FromDays(30)), To = DateTime.Now
        };

        private readonly Checkout.Previous.ICheckoutApi _productionApi = CheckoutSdk.Builder()
            .Previous()
            .StaticKeys()
            .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY_PROD"))
            .Environment(Environment.Production)
            .LogProvider(TestLoggerFactoryHelper.Create())
            .Build();

        [Fact(Skip = "Only works in Production")]
        private async Task ShouldQueryPaymentsReport()
        {
            var query = new ReconciliationQueryPaymentsFilter
            {
                From = DateTime.Now.Subtract(TimeSpan.FromDays(30)), To = DateTime.Now
            };

            var response = await _productionApi.ReconciliationClient().QueryPaymentsReport(query);
            response.ShouldNotBeNull();
            response.Count.ShouldNotBeNull();
            response.Count.GetValueOrDefault().ShouldBeGreaterThan(0);
            response.Data.ShouldNotBeEmpty();
        }

        [Fact(Skip = "Only works in Production")]
        private async Task ShouldSinglePaymentReport()
        {
            var response = await _productionApi.ReconciliationClient()
                .SinglePaymentReport("pay_123456");
            response.ShouldNotBeNull();
            response.Count.ShouldNotBeNull();
            response.Count.GetValueOrDefault().ShouldBeGreaterThan(0);
            response.Data.ShouldNotBeEmpty();
        }

        [Fact(Skip = "Only works in Production")]
        private async Task ShouldQueryStatementsReport()
        {
            var response = await _productionApi.ReconciliationClient().QueryStatementsReport(_queryFilterDateRange);
            response.ShouldNotBeNull();
            response.Count.ShouldNotBeNull();
            response.Count.GetValueOrDefault().ShouldBeGreaterThan(0);
            response.Data.ShouldNotBeEmpty();
        }

        [Fact(Skip = "Only works in Production")]
        private async Task ShouldRetrieveCsvPaymentReport()
        {
            var response = await _productionApi.ReconciliationClient().RetrieveCsvPaymentReport(_queryFilterDateRange);
            response.ShouldNotBeNull();
            response.Content.ShouldNotBeEmpty();
        }

        [Fact(Skip = "Only works in Production")]
        private async Task ShouldRetrieveCsvSingleStatementReport()
        {
            var response = await _productionApi.ReconciliationClient()
                .RetrieveCsvSingleStatementReport("reference");
            response.ShouldNotBeNull();
            response.Content.ShouldNotBeEmpty();
        }

        [Fact(Skip = "Only works in Production")]
        private async Task ShouldRetrieveCsvStatementsReport()
        {
            var response = await _productionApi.ReconciliationClient()
                .RetrieveCsvStatementsReport(_queryFilterDateRange);
            response.ShouldNotBeNull();
            response.Content.ShouldNotBeEmpty();
        }
    }
}