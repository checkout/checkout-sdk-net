using Checkout.Instruments;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Instruments
{
    public class GetInstrumentTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public GetInstrumentTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanCreateInstrument()
        {
            var cardTokenRequest = TestHelper.CreateCardTokenRequest();
            var cardTokenResponse = await _api.Tokens.RequestAToken(cardTokenRequest);
            var instrumentRequest = new InstrumentRequest("token", cardTokenResponse.Token);

            var createInstrumentResponse = await _api.Instruments.CreateAnInstrument(instrumentRequest);

            createInstrumentResponse.ShouldNotBeNull();
            createInstrumentResponse.ExpiryMonth.ShouldBe(cardTokenRequest.ExpiryMonth);
            createInstrumentResponse.ExpiryYear.ShouldBe(cardTokenRequest.ExpiryYear);
            createInstrumentResponse.Last4.ShouldBe(cardTokenResponse.Last4);

            var getInstrumentResponse = await _api.Instruments.GetInstrumentDetails(createInstrumentResponse.Id);

            getInstrumentResponse.ShouldNotBeNull();
            getInstrumentResponse.Id.ShouldBe(createInstrumentResponse.Id);
        }
    }
}
