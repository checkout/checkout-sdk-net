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
        private CardAuthorizationResponse _transaction;

        public async Task InitializeAsync()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest cardRequest = await CreateVirtualCard(cardholderResponse.Id);
            _cardRequest = await Api.IssuingClient().CreateCard(cardRequest);
            await Api.IssuingClient().ActivateCard(_cardRequest.Id);
            CardAuthorizationResponse _transaction = await CardAuthorizationResponse();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private Task ShouldSimulateAuthorization()
        {
            _transaction.HttpStatusCode.ShouldBe(201);
            _transaction.Body.ShouldNotBeNull();
            _transaction.ResponseHeaders.ShouldNotBeNull();
            _transaction.Id.ShouldNotBeNull();
            _transaction.Status.ShouldBe(TransactionStatus.Authorized);
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldSimulateIncrementingAuthorization()
        {
            CardIncrementAuthorizationRequest cardIncrementAuthorizationRequest = new CardIncrementAuthorizationRequest
            {
                Amount = 1
            };

            CardIncrementAuthorizationResponse response =
                await Api.IssuingClient().SimulateIncrementingAuthorization(_transaction.Id, cardIncrementAuthorizationRequest);

            response.HttpStatusCode.ShouldBe(201);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Status.ShouldBe(TransactionStatus.Authorized);
        }
        
        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldSimulateClearing()
        {
            CardClearingAuthorizationRequest cardClearingAuthorizationRequest = new CardClearingAuthorizationRequest
            {
                Amount = 1
            };

            EmptyResponse response =
                await Api.IssuingClient().SimulateClearing(_transaction.Id, cardClearingAuthorizationRequest);

            response.HttpStatusCode.ShouldBe(202);
            response.ResponseHeaders.ShouldNotBeNull();
        }
        
        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldSimulateReversal()
        {
            CardReversalAuthorizationRequest cardReversalAuthorizationRequest = new CardReversalAuthorizationRequest
            {
                Amount = 1
            };

            CardReversalAuthorizationResponse response =
                await Api.IssuingClient().SimulateReversal(_transaction.Id, cardReversalAuthorizationRequest);

            response.HttpStatusCode.ShouldBe(201);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Status.ShouldBe(ReversalStatus.Reversed);
        }
        
        private async Task<CardAuthorizationResponse> CardAuthorizationResponse()
        {
            CardAuthorizationRequest cardAuthorizationRequest = new CardAuthorizationRequest
            {
                Card = new CardSimulation
                    { Id = _cardRequest.Id, ExpiryMonth = _cardRequest.ExpiryMonth, ExpiryYear = _cardRequest.ExpiryYear },
                Transaction = new TransactionSimulation
                {
                    Type = TransactionType.Purchase, Amount = 100, Currency = Currency.GBP
                }
            };

            CardAuthorizationResponse response =
                await Api.IssuingClient().SimulateAuthorization(cardAuthorizationRequest);
            return response;
        }
    }
}