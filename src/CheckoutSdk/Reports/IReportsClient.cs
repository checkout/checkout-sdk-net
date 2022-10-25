using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reports
{
    public interface IReportsClient
    {
        Task<ReportsResponse> GetAllReports(ReportsQuery query, CancellationToken cancellationToken = default);

        Task<ReportDetailsResponse> GetReportDetails(string reportId, CancellationToken cancellationToken = default);
    }
}
