using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Disputes
{
    public class DebugDisputesEndpoint
    {
        [Fact]
        public async Task Dummy()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://api.sandbox.checkout.com/disputes");
                response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
            }
        }
    }
}
