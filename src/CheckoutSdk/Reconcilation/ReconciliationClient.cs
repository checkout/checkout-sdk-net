using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reconciliation
{
	public class ReconciliationClient : AbstractClient, IReconciliationClient
	{
		private const string REPORTING = "reporting";
		private const string PAYMENTS = "payments";
		private const string STATEMENTS = "statements";
		private const string DOWNLOAD = "download";

		public ReconciliationClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
		{
		}

		public Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter)
		{
			return ApiClient.Query<ReconciliationPaymentReportResponse>(BuildPath(REPORTING, PAYMENTS), SdkAuthorization(), filter);
		}

		public Task<ReconciliationPaymentReportResponse> SinglePaymentReportAsync(string paymentId)
		{
			return ApiClient.Query<ReconciliationPaymentReportResponse>(BuildPath(REPORTING, PAYMENTS, paymentId), SdkAuthorization(),paymentId);
		}

		public Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter)
		{
			return ApiClient.Query<StatementReportResponse>(BuildPath(REPORTING, STATEMENTS), SdkAuthorization(), filter);
		}

		public Task<string> RetrieveCSVPaymentReport(CancellationToken cancellationToken)
		{
			return ApiClient.Get<string>(BuildPath(REPORTING, PAYMENTS, DOWNLOAD), SdkAuthorization(), cancellationToken);
		}

		public Task<string> RetrieveCSVSingleStatementReport(string statementId, CancellationToken cancellationToken)
		{
			return ApiClient.Get<string>(BuildPath(REPORTING, STATEMENTS, statementId, PAYMENTS, DOWNLOAD), SdkAuthorization(), cancellationToken);
		}

		public Task<string> RetrieveCSVStatementsReport(CancellationToken cancellationToken)
		{
			return ApiClient.Get<string>(BuildPath(REPORTING, STATEMENTS, DOWNLOAD), SdkAuthorization(), cancellationToken);
		}

	}

}