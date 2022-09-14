using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Reports
{
    public class ReportsClient : AbstractClient, IReportsClient
    {
        private const string ReportsPath = "reports";

        public ReportsClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<ReportsResponse> GetAllReports(
            ReportsQuery query, 
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Query<ReportsResponse>(ReportsPath, SdkAuthorization(), query, cancellationToken);
        }

        public Task<ReportDetailsResponse> GetReportDetails(
            string reportId,
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<ReportDetailsResponse>(
                BuildPath(ReportsPath, reportId), 
                SdkAuthorization(),
                cancellationToken);
        }
    }
}
