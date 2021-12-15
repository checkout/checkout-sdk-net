using Checkout.Common;
using System.Threading.Tasks;

namespace Checkout.Reconciliation
{
    public interface IReconciliationClient
    {
        Task<ReconciliationPaymentReportResponse> QueryPaymentsReport(ReconciliationQueryPaymentsFilter filter);

        Task<ReconciliationPaymentReportResponse> SinglePaymentReport(string paymentId);

        Task<StatementReportResponse> QueryStatementsReport(QueryFilterDateRange filter);
    }
}