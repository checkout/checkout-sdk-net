using Castle.Core.Internal;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Reports
{
    public class ReportsIntegrationTest : SandboxTestFixture
    {
        public ReportsIntegrationTest() : base(PlatformType.Default)
        {
        }
        
        private readonly ReportsQuery _query = new ReportsQuery
        {
            CreatedAfter = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
            CreatedBefore = DateTime.Now
        };

        [Fact]
        private async Task ShouldGetAllReports()
        {
            var reports = await DefaultApi.ReportsClient().GetAllReports(_query);
            
            reports.ShouldNotBeNull();
            if (!reports.Data.IsNullOrEmpty())
            {
                foreach (var report in reports.Data)
                {
                    report.Id.ShouldNotBeNull();
                    report.CreatedOn.ShouldNotBeNull();
                    report.Type.ShouldNotBeNull();
                    report.Account.ShouldNotBeNull();
                    report.Account.ClientId.ShouldNotBeNull();
                    report.Account.EntityId.ShouldNotBeNull();
                    report.From.ShouldNotBeNull();
                    report.To.ShouldNotBeNull();
                    report.Files.ShouldNotBeNull();
                }
            }
        }

        [Fact]
        private async Task ShouldGetReportDetails()
        {
            var reports = await DefaultApi.ReportsClient().GetAllReports(_query);
            
            reports.ShouldNotBeNull();
            if (!reports.Data.IsNullOrEmpty())
            {
                var reportDetails = reports.Data[0];
                var detailsResponse = await DefaultApi.ReportsClient().GetReportDetails(reportDetails.Id);
                
                detailsResponse.ShouldNotBeNull();
                detailsResponse.Id.ShouldBe(reportDetails.Id);
                detailsResponse.CreatedOn.ShouldBe(reportDetails.CreatedOn);
                detailsResponse.Type.ShouldBe(reportDetails.Type);
                detailsResponse.Account.ClientId.ShouldBe(reportDetails.Account.ClientId);
                detailsResponse.Account.EntityId.ShouldBe(reportDetails.Account.EntityId);
                detailsResponse.From.ShouldBe(reportDetails.From);
                detailsResponse.To.ShouldBe(reportDetails.To);
            }
        }
    }
}
