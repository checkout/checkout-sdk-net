using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reconciliation
{
    public interface IReconciliationClient
    {
        Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter,
            CancellationToken cancellationToken = default);

        Task<ReconciliationPaymentReportResponse> SinglePaymentReport(string paymentId,
            CancellationToken cancellationToken = default);

        Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter,
            CancellationToken cancellationToken = default);

        Task<ContentsResponse> RetrieveCsvPaymentReport(QueryFilterDateRange filter,
            CancellationToken cancellationToken = default);

        Task<ContentsResponse> RetrieveCsvSingleStatementReport(string statementId,
            CancellationToken cancellationToken = default);

        Task<ContentsResponse> RetrieveCsvStatementsReport(QueryFilterDateRange filter,
            CancellationToken cancellationToken = default);
    }
}