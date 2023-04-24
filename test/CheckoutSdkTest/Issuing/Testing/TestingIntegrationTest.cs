using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Testing.Requests;
using Checkout.Issuing.Testing.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Testing
{
    public class CardTestingIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private CardResponse _cardRequest;

        public async Task InitializeAsync()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest cardRequest = await CreateVirtualCard(cardholderResponse.Id);
            _cardRequest = await Api.IssuingClient().CreateCard(cardRequest);
            await Api.IssuingClient().ActivateCard(_cardRequest.Id);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldSimulateAuthorization()
        {
            CardAuthorizationRequest cardAuthorizationRequest = new CardAuthorizationRequest
            {
                Card = new CardSimulation { Id = _cardRequest.Id, ExpiryMonth = _cardRequest.ExpiryMonth, ExpiryYear = _cardRequest.ExpiryYear },
                Transaction = new TransactionSimulation
                {
                    Type = TransactionType.Purchase, Amount = 100, Currency = Currency.GBP
                }
            };

            CardAuthorizationResponse response =
                await Api.IssuingClient().SimulateAuthorization(cardAuthorizationRequest);

            response.HttpStatusCode.ShouldBe(201);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Status.ShouldBe(TransactionStatus.Authorized);
        }
    }
}