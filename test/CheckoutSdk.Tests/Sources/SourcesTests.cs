using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
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
            sourceResponse.Customer.ShouldBeOfType<CustomerResponse>();
            sourceResponse.Id.ShouldNotBeNullOrEmpty();
            sourceResponse.Links.ShouldBeOfType<Dictionary<string, Link>>();
            sourceResponse.ResponseCode.ShouldBe("10000");
            sourceResponse.ResponseData.ShouldNotBeNull();
            sourceResponse.Type.ToLower().ShouldBe(sourceRequest.Type.ToLower());
        }
    }
}