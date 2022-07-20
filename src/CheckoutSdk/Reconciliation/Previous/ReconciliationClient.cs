using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reconciliation.Previous
{
    public class ReconciliationClient : AbstractClient, IReconciliationClient
    {
        private const string ReportingPath = "reporting";
        private const string PaymentsPath = "payments";
        private const string DownloadPath = "download";
        private const string StatementsPath = "statements";

        public ReconciliationClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<ReconciliationPaymentReportResponse>(BuildPath(ReportingPath, PaymentsPath),
                SdkAuthorization(), filter, cancellationToken);
        }

        public Task<ReconciliationPaymentReportResponse> SinglePaymentReport(string paymentId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Get<ReconciliationPaymentReportResponse>(BuildPath(ReportingPath, PaymentsPath, paymentId),
                SdkAuthorization(), cancellationToken);
        }

        public Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<StatementReportResponse>(BuildPath(ReportingPath, StatementsPath),
                SdkAuthorization(), filter, cancellationToken);
        }

        public Task<ContentsResponse> RetrieveCsvPaymentReport(QueryFilterDateRange filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<ContentsResponse>(BuildPath(ReportingPath, PaymentsPath, DownloadPath), SdkAuthorization(),
                filter,
                cancellationToken);
        }

        public Task<ContentsResponse> RetrieveCsvSingleStatementReport(string statementId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("statementId", statementId);
            return ApiClient.Query<ContentsResponse>(
                BuildPath(ReportingPath, StatementsPath, statementId, PaymentsPath, DownloadPath), SdkAuthorization(),
                null,
                cancellationToken);
        }

        public Task<ContentsResponse> RetrieveCsvStatementsReport(QueryFilterDateRange filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<ContentsResponse>(
                BuildPath(ReportingPath, StatementsPath, DownloadPath), SdkAuthorization(), filter,
                cancellationToken);
        }
    }
}