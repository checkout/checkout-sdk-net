using Checkout.Common;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Reconciliation.Previous
{
    public class ReconciliationClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly ReconciliationClient _reconciliationClient;

        private readonly DateTime _from = new DateTime(2019, 2, 22, 12, 31, 44, 0, DateTimeKind.Utc);
        private readonly DateTime _to = new DateTime(2021, 12, 30, 13, 21, 34, 0, DateTimeKind.Utc);
        private const string Reference = "ORD-5023-4E89";
        private const string Csv = "csv_content";

        public ReconciliationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);

            _reconciliationClient = new ReconciliationClient(_apiClient.Object, configuration.Object);
        }

        [Fact]
        private async Task ShouldQueryPaymentReport()
        {
            //Arrange
            var request = new ReconciliationQueryPaymentsFilter
            {
                Limit = 200, Reference = Reference, To = _to, From = _from
            };

            var returnResponse = new ReconciliationPaymentReportResponse
            {
                Count = 5, Data = new List<PaymentReportData>()
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
                Count = 5, Data = new List<PaymentReportData>()
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ReconciliationPaymentReportResponse>($"reporting/payments/{paymentId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => returnResponse);

            //Act
            var response = await _reconciliationClient.SinglePaymentReport(paymentId);

            //Assert
            response.ShouldNotBeNull();
            response.Count.ShouldBe(returnResponse.Count);
        }

        [Fact]
        private async Task ShouldQueryStatementsReport()
        {
            //Arrange
            var request = new QueryFilterDateRange {To = _to, From = _from};

            var returnResponse = new StatementReportResponse {Count = 5, Data = new List<StatementData>()};

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
        private async Task ShouldRetrieveCsvPaymentReport()
        {
            var request = new QueryFilterDateRange {To = _to, From = _from};

            _apiClient.Setup(apiClient =>
                    apiClient.Query<ContentsResponse>("reporting/payments/download", _authorization, request,
                        CancellationToken.None))
                .ReturnsAsync(() => new ContentsResponse {Content = Csv});

            var response = await _reconciliationClient.RetrieveCsvPaymentReport(request);

            response.ShouldNotBeNull();
            response.Content.ShouldBe(Csv);
        }

        [Fact]
        private async Task ShouldRetrieveCsvSingleStatementReport()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Query<ContentsResponse>("reporting/statements/id/payments/download", _authorization,
                        It.IsAny<QueryFilterDateRange>(),
                        CancellationToken.None))
                .ReturnsAsync(() => new ContentsResponse {Content = Csv});

            var response = await _reconciliationClient.RetrieveCsvSingleStatementReport("id");
            response.ShouldNotBeNull();
            response.Content.ShouldBe(Csv);
        }

        [Fact]
        private async Task ShouldRetrieveCsvStatementsReport()
        {
            var request = new QueryFilterDateRange {To = _to, From = _from};

            _apiClient.Setup(apiClient =>
                    apiClient.Query<ContentsResponse>("reporting/statements/download", _authorization, request,
                        CancellationToken.None))
                .ReturnsAsync(() => new ContentsResponse {Content = Csv});

            var response = await _reconciliationClient.RetrieveCsvStatementsReport(request);

            response.ShouldNotBeNull();
            response.Content.ShouldBe(Csv);
        }
    }
}