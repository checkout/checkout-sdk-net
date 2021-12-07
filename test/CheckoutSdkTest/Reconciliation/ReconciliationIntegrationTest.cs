using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Reconciliation.Tests
{
    public class ReconciliationIntegrationTest : SandboxTestFixture
    {
        public ReconciliationIntegrationTest() : base(PlatformType.Default)
        {
        }

        private readonly DateTime _from = new DateTime(2020, 2, 22, 12, 31, 44, 0, DateTimeKind.Utc);
        private readonly DateTime _to = new DateTime(2020, 2, 26, 13, 21, 34, 0, DateTimeKind.Utc);
        const string _paymentId = "pay_nezg6bx2k22utmk4xm5s2ughxi";
        const string _reference = "ORD-5023-4E89";
        const string _actionId = "act_nezg6bx2k22utmk4xm5s2ughxi";
        const string _actionType = "SdkAuthorization";
        const string _breakdownType = "Gateway Fee Tax ARE USD/GBP@0.7640412612";
        const string _statementId = "190110B107654";

        [Fact]
        private async Task ShouldQueryPaymentreport()
        {
            var request = new ReconciliationQueryPaymentsFilter
            {
                Limit = 200,
                Reference = _reference,
                To = _to,
                From = _from
            };
            var response = await DefaultApi.ReconciliationClient().QueryPaymentsReport(request);
            response.ShouldNotBeNull();

            response.Count.ShouldBe(1);
        }

        [Fact]
        private async Task ShouldQuerySinglePaymentReport()
        {
            var response = await DefaultApi.ReconciliationClient().SinglePaymentReportAsync("Payment");
            response.ShouldNotBeNull();

            response.Count.ShouldBe(1);
        }

        [Fact]
        private async Task RetrieveCSVPaymentReport()
        {
            CancellationToken ct = new CancellationToken();

            var response = await DefaultApi.ReconciliationClient().RetrieveCSVPaymentReport(ct);
            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetrieveCSVSingleStatementReport()
        {
            CancellationToken ct = new CancellationToken();

            var response = await DefaultApi.ReconciliationClient().RetrieveCSVSingleStatementReport("stid", ct);
            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetrieveCSVStatementsReport()
        {
            CancellationToken ct = new CancellationToken();

            var response = await DefaultApi.ReconciliationClient().RetrieveCSVStatementsReport(ct);
            response.ShouldNotBeNull();
        }
    }
}