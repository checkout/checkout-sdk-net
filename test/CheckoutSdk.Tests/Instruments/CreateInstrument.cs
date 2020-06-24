using Checkout.Instruments;
using Checkout.Payments;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Instruments
{
    public class CreateInstrumentTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public CreateInstrumentTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanCreateInstrument()
        {
            var cardTokenRequest = TestHelper.CreateCardTokenRequest();
            var cardTokenResponse = await _api.Tokens.RequestAsync(cardTokenRequest);
            var instrumentRequest = new InstrumentRequest("token", cardTokenResponse.Token);

            var instrumentResponse = await _api.Instruments.CreateAsync(instrumentRequest);

            instrumentResponse.ShouldNotBeNull();
            instrumentResponse.ExpiryMonth.ShouldBe(cardTokenRequest.ExpiryMonth);
            instrumentResponse.ExpiryYear.ShouldBe(cardTokenRequest.ExpiryYear);
            instrumentResponse.Last4.ShouldBe(cardTokenResponse.Last4);
        }
    }
}
