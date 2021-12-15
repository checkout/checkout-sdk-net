using Checkout.Common;
using System.Threading.Tasks;

namespace Checkout.Reconciliation
{
    public class ReconciliationClient : AbstractClient, IReconciliationClient
    {
        private const string ReportingPath = "reporting";
        private const string PaymentsPath = "payments";
        private const string StatementsPath = "statements";

        public ReconciliationClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<ReconciliationPaymentReportResponse>(BuildPath(ReportingPath, PaymentsPath),
                SdkAuthorization(), filter);
        }

        public Task<ReconciliationPaymentReportResponse> SinglePaymentReport(string paymentId)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Get<ReconciliationPaymentReportResponse>(BuildPath(ReportingPath, PaymentsPath, paymentId),
                SdkAuthorization());
        }

        public Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<StatementReportResponse>(BuildPath(ReportingPath, StatementsPath),
                SdkAuthorization(), filter);
        }
    }
}