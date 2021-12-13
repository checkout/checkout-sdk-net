using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reconciliation
{
    public interface IReconciliationClient
    {
        Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter);

        Task<ReconciliationPaymentReportResponse> SinglePaymentReportAsync(string paymentId);

        Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter);

        Task<string> RetrieveCSVPaymentReport(CancellationToken cancellationToken);

        Task<string> RetrieveCSVSingleStatementReport(string statementId, CancellationToken cancellationToken);

        Task<string> RetrieveCSVStatementsReport(CancellationToken cancellationToken);
    }
}