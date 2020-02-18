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
            GetDisputesResponse getDisputesResponse = await _api.Disputes.GetDisputesAsync();

            getDisputesResponse.ShouldNotBeNull();
            getDisputesResponse.Limit.ShouldBe(50);
            getDisputesResponse.Skip.ShouldBe(0);
            getDisputesResponse.ThisChannelOnly.ShouldBeFalse();
        }

        [Fact]
        public async Task GivenInvalidDisputeIdShouldReturnZeroDisputes()
        {
            GetDisputesResponse getDisputesResponse = await _api.Disputes.GetDisputesAsync(id: "invalid");

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
            Should.Throw<CheckoutApiException>(async () => await _api.Disputes.GetDisputesAsync(limit: 251));
        }
    }
}
