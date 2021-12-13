using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Common;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout.Reconciliation.Tests
{
    public class ReconciliationClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        private readonly ReconciliationClient _reconciliationClient;

        private readonly DateTime _from = new DateTime(2019, 2, 22, 12, 31, 44, 0, DateTimeKind.Utc);
        private readonly DateTime _to = new DateTime(2021, 12, 30, 13, 21, 34, 0, DateTimeKind.Utc);
        const string _paymentId = "pay_nezg6bx2k22utmk4xm5s2ughxi";
        const string _reference = "ORD-5023-4E89";
        const string _actionId = "act_nezg6bx2k22utmk4xm5s2ughxi";
        const string _actionType = "SdkAuthorization";
        const string _breakdownType = "Gateway Fee Tax ARE USD/GBP@0.7640412612";
        const string _statementId = "190110B107654";

        public ReconciliationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);

            _reconciliationClient = new ReconciliationClient(_apiClient.Object, _configuration.Object);
        }

        [Fact]
        private async Task ShouldQueryPaymentreport()
        {
            //Arrange
            var request = new ReconciliationQueryPaymentsFilter
            {
                Limit = 200,
                Reference = _reference,
                To = _to,
                From = _from
            };

            var returnResponse = new ReconciliationPaymentReportResponse
            {
                Count = 5,
                Data = new List<PaymentReportData>()
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Query<ReconciliationPaymentReportResponse>("reporting/payments", _authorization, request,
                        CancellationToken.None))
                .ReturnsAsync(() => returnResponse);
            
            //Act
            var response = await _reconciliationClient.QueryPaymentsReport(request);

            //Assert
            response.ShouldNotBeNull();
            response.Count.ShouldBe(returnResponse.Count);
        }

        [Fact]
        private async Task ShouldQuerySinglePaymentReport()
        {
            //Arrange
            string paymentId = "1";

            var returnResponse = new ReconciliationPaymentReportResponse
            {
                Count = 5,
                Data = new List<PaymentReportData>()
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Query<ReconciliationPaymentReportResponse>($"reporting/payments/{paymentId}", _authorization, paymentId,
                        CancellationToken.None))
                .ReturnsAsync(() => returnResponse);

            //Act
            var response = await _reconciliationClient.SinglePaymentReportAsync(paymentId);

            //Assert
            response.ShouldNotBeNull();
            response.Count.ShouldBe(returnResponse.Count);
        }

        [Fact]
        private async Task ShouldQueryStatementsReport()
        {
            //Arrange
            var request = new QueryFilterDateRange
            {
                To = _to,
                From = _from
            };

            var returnResponse = new StatementReportResponse
            {
                Count = 5,
                Data = new List<StatementData>()
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Query<StatementReportResponse>("reporting/statements", _authorization, request,
                        CancellationToken.None))
                .ReturnsAsync(() => returnResponse);

            //Act
            var response = await _reconciliationClient.QueryStatementsReport(request);

            //Assert
            response.ShouldNotBeNull();
            response.Count.ShouldBe(returnResponse.Count);
        }

        [Fact]
        private async Task RetrieveCSVPaymentReport()
        {
            //Arrange
            CancellationToken ct = new CancellationToken();

            var returnResponse = "done";

            _apiClient.Setup(apiClient =>
                    apiClient.Get<string>("reporting/payments/download", _authorization, ct))
                .ReturnsAsync(() => returnResponse);

            //Act
            var response = await _reconciliationClient.RetrieveCSVPaymentReport(ct);

            //Assert
            response.ShouldNotBeNull();
            response.ShouldBe(returnResponse);
        }

        [Fact]
        private async Task ShouldRetrieveCSVSingleStatementReport()
        {
            //Arrange
            CancellationToken ct = new CancellationToken();
            var statementId = "1";

            var returnResponse = "done";

            _apiClient.Setup(apiClient =>
                    apiClient.Get<string>($"reporting/statements/{statementId}/payments/download", _authorization, ct))
                .ReturnsAsync(() => returnResponse);

            //Act
            var response = await _reconciliationClient.RetrieveCSVSingleStatementReport(statementId, ct);

            //Assert
            response.ShouldNotBeNull();
            response.ShouldBe(returnResponse);
        }

        [Fact]
        private async Task ShouldRetrieveCSVStatementsReport()
        {
            //Arrange
            CancellationToken ct = new CancellationToken();
            var returnResponse = "done";

            _apiClient.Setup(apiClient =>
                    apiClient.Get<string>($"reporting/statements/download", _authorization, ct))
                .ReturnsAsync(() => returnResponse);

            //Act
            var response = await _reconciliationClient.RetrieveCSVStatementsReport(ct);

            //Assert
            response.ShouldNotBeNull();
            response.ShouldBe(returnResponse);
        }
    }
}