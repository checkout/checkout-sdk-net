using Checkout.Payments;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Sources
{
    public class SourcesTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public SourcesTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanRequestSource()
        {
            var sourceRequest = TestHelper.CreateSourceRequest();
            var sourceResponse = await _api.Sources.RequestAsync(sourceRequest);

            sourceResponse.ShouldNotBeNull();
            sourceResponse.ResponseCode.ShouldBe("10000");
            sourceResponse.Type.ToLower().ShouldBe(sourceRequest.Type.ToLower());
            sourceResponse.ResponseData.ShouldNotBeNull();
        }
    }
}