using Checkout.Common;
using Checkout.Sources;
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
            var source = sourceResponse.Source;
            source.Customer.ShouldBeOfType<CustomerResponse>();
            source.Id.ShouldNotBeNullOrEmpty();
            source.Links.ShouldBeOfType<Dictionary<string, Link>>();
            source.ResponseCode.ShouldBe("10000");
            source.ResponseData.ShouldNotBeNull();
            source.Type.ToLower().ShouldBe(sourceRequest.Type.ToLower());
        }
    }
}
