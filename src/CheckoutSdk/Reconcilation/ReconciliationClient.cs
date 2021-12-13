using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reconciliation
{
    public class ReconciliationClient : AbstractClient, IReconciliationClient
    {
        private const string ReportingPath = "reporting";
        private const string PaymentsPath = "payments";
        private const string StatementsPath = "statements";
        private const string DownloadPath = "download";

        public ReconciliationClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<ReconciliationPaymentReportResponse>(BuildPath(ReportingPath, PaymentsPath), SdkAuthorization(), filter);
        }

        public Task<ReconciliationPaymentReportResponse> SinglePaymentReportAsync(string paymentId)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Query<ReconciliationPaymentReportResponse>(BuildPath(ReportingPath, PaymentsPath, paymentId), SdkAuthorization(), paymentId);
        }

        public Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<StatementReportResponse>(BuildPath(ReportingPath, StatementsPath), SdkAuthorization(), filter);
        }

        public Task<string> RetrieveCSVPaymentReport(CancellationToken cancellationToken)
        {
            return ApiClient.Get<string>(BuildPath(ReportingPath, PaymentsPath, DownloadPath), SdkAuthorization(), cancellationToken);
        }

        public Task<string> RetrieveCSVSingleStatementReport(string statementId, CancellationToken cancellationToken)
        {
            CheckoutUtils.ValidateParams("statementId", statementId);
            return ApiClient.Get<string>(BuildPath(ReportingPath, StatementsPath, statementId, PaymentsPath, DownloadPath), SdkAuthorization(), cancellationToken);
        }

        public Task<string> RetrieveCSVStatementsReport(CancellationToken cancellationToken)
        {
            return ApiClient.Get<string>(BuildPath(ReportingPath, StatementsPath, DownloadPath), SdkAuthorization(), cancellationToken);
        }
    }
}