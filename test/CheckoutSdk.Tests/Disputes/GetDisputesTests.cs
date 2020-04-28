using System.Collections;
using System.Threading.Tasks;
using Checkout.Disputes;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Disputes
{
    public class GetDisputesTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public GetDisputesTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanGetDisputes()
        {
            var getDisputesRequest = new GetDisputesRequest();
            var getDisputesResponse = await _api.Disputes.GetDisputesAsync(getDisputesRequest: getDisputesRequest);

            getDisputesResponse.ShouldNotBeNull();
            getDisputesResponse.Limit.ShouldBe(50);
            getDisputesResponse.Skip.ShouldBe(0);
            getDisputesResponse.ThisChannelOnly.ShouldBeFalse();
        }

        [Fact]
        public async Task GivenInvalidDisputeIdShouldReturnZeroDisputes()
        {
            var getDisputesRequest = new GetDisputesRequest(id: "invalid");
            var getDisputesResponse = await _api.Disputes.GetDisputesAsync(getDisputesRequest: getDisputesRequest);

            getDisputesResponse.ShouldNotBeNull();
            getDisputesResponse.Limit.ShouldBe(50);
            getDisputesResponse.Skip.ShouldBe(0);
            getDisputesResponse.ThisChannelOnly.ShouldBeFalse();
            getDisputesResponse.TotalCount.ShouldBe(0);
            (getDisputesResponse.Data as IList).Count.ShouldBe(0);
        }

        [Fact]
        public void GivenLimitOutOfBoundShouldThrowCheckoutApiException()
        {
            var getDisputesRequest = new GetDisputesRequest(limit: 251);
            var checkoutApiException = Should.Throw<CheckoutApiException>(async () => await _api.Disputes.GetDisputesAsync(getDisputesRequest: getDisputesRequest));
            
            checkoutApiException.ShouldNotBeNull();
            checkoutApiException.HttpStatusCode.ShouldBe((System.Net.HttpStatusCode)422);
        }
    }
}
