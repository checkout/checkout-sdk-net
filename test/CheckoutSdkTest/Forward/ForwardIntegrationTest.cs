using Checkout.Forward.Requests;
using Checkout.Forward.Requests.Sources;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Forward
{
    public class ForwardIntegrationTest : SandboxTestFixture
    {
        public ForwardIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test requires a valid id or Token source")]
        private async Task ShouldForwardAnApiRequest()
        {
            var forwardRequest = CreateForwardRequest();

            var forwardResponse = await DefaultApi.ForwardClient().ForwardAnApiRequest(forwardRequest);

            forwardResponse.ShouldNotBeNull();
            forwardResponse.RequestId.ShouldNotBeNull();
            forwardResponse.DestinationResponse.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid id or Token source")]
        private async Task ShouldGetForwardRequest()
        {
            var forwardRequest = CreateForwardRequest();

            var forwardResponse = await DefaultApi.ForwardClient().ForwardAnApiRequest(forwardRequest);

            var getForwardResponse = await DefaultApi.ForwardClient().GetForwardRequest(forwardResponse.RequestId);

            getForwardResponse.ShouldNotBeNull();
            getForwardResponse.RequestId.ShouldNotBeNull();
            getForwardResponse.EntityId.ShouldNotBeNull();
            getForwardResponse.DestinationRequest.ShouldNotBeNull();
            getForwardResponse.CreatedOn.ShouldNotBeNull();
            getForwardResponse.Reference.ShouldNotBeNull();
            getForwardResponse.DestinationResponse.ShouldNotBeNull();
        }

        private ForwardRequest CreateForwardRequest()
        {
            return new ForwardRequest
            {
                Source = new IdSource { Id = "src_v5rgkf3gdtpuzjqesyxmyodnya", },
                Reference = "ORD-5023-4E89",
                ProcessingChannelId = "pc_azsiyswl7bwe2ynjzujy7lcjca",
                NetworkToken = new NetworkToken { Enabled = true, RequestCryptogram = false },
                DestinationRequest = new DestinationRequest
                {
                    Url = "https://example.com/payments",
                    Method = MethodType.Post,
                    Headers =
                        new Headers()
                        {
                            Encrypted = "<JWE encrypted JSON object with string values>",
                            Raw =
                                new Dictionary<string, string>
                                {
                                    { "Idempotency-Key", "xe4fad12367dfgrds" },
                                    { "Content-Type", "application/json" }
                                }
                        },
                    Body =
                        "{\"amount\": 1000, \"currency\": \"USD\", \"reference\": \"some_reference\", \"source\": {\"type\": \"card\", \"number\": \"{{card_number}}\", \"expiry_month\": \"{{card_expiry_month}}\", \"expiry_year\": \"{{card_expiry_year_yyyy}}\", \"name\": \"Ali Farid\"}, \"payment_type\": \"Regular\", \"authorization_type\": \"Final\", \"capture\": true, \"processing_channel_id\": \"pc_xxxxxxxxxxx\", \"risk\": {\"enabled\": false}, \"merchant_initiated\": true}"
                }
            };
        }
    }
}