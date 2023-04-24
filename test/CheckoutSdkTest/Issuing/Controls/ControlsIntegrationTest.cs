using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Controls.Requests;
using Checkout.Issuing.Controls.Requests.Create;
using Checkout.Issuing.Controls.Requests.Query;
using Checkout.Issuing.Controls.Requests.Update;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Issuing.Controls.Responses.Query;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Controls
{
    public class CardControlsIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private CardResponse _cardRequest;
        private CardControlResponse _cardControl;

        public async Task InitializeAsync()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest cardRequest = await CreateVirtualCard(cardholderResponse.Id);
            _cardRequest = await Api.IssuingClient().CreateCard(cardRequest);

            await Api.IssuingClient().ActivateCard(_cardRequest.Id);

            CardControlRequest cardControlRequest = new VelocityCardControlRequest
            {
                Description = "Max spend of 500€ per week for restaurants",
                TargetId = _cardRequest.Id,
                VelocityLimit = new VelocityLimit
                {
                    AmountLimit = 500,
                    VelocityWindow = new VelocityWindow { Type = VelocityWindowType.Monthly },
                    MccList = new List<string> { "4121", "4582" }
                }
            };

            _cardControl = await Api.IssuingClient().CreateCardControl(cardControlRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private void ShouldCreateCardControl()
        {
            CardControlResponse response = _cardControl;

            response.ShouldNotBeNull();
            response.TargetId.ShouldBe(_cardRequest.Id);
            response.ControlType.ShouldBe(ControlType.VelocityLimit);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCardControls()
        {
            CardControlQueryTarget cardControlQueryTarget = new CardControlQueryTarget { TargetId = _cardRequest.Id };

            CardControlsQueryResponse response = await Api.IssuingClient().GetCardControls(cardControlQueryTarget);

            response.ShouldNotBeNull();
            response.Controls.ShouldNotBeNull();
            foreach (CardControlResponse control in response.Controls)
            {
                control.TargetId.ShouldBe(_cardRequest.Id);
            }
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetControlDetails()
        {
            CardControlResponse response = await Api.IssuingClient().GetCardControlDetails(_cardControl.Id);

            response.ShouldNotBeNull();
            response.TargetId.ShouldBe(_cardRequest.Id);
            response.Id.ShouldBe(_cardControl.Id);
            response.ControlType.ShouldBe(ControlType.VelocityLimit);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldUpdateControl()
        {
            const string description = "New max spend of 1000€ per month for restaurants";
            UpdateCardControlRequest updateCardControlRequest = new UpdateCardControlRequest
            {
                Description = description,
                VelocityLimit = new VelocityLimit
                {
                    AmountLimit = 1000,
                    VelocityWindow = new VelocityWindow { Type = VelocityWindowType.Monthly }
                },
                MccLimit = null
            };

            CardControlResponse response =
                await Api.IssuingClient().UpdateCardControl(_cardControl.Id, updateCardControlRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(_cardControl.Id);
            response.Description.ShouldBe(description);
            response.ControlType.ShouldBe(ControlType.VelocityLimit);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldRemoveControl()
        {
            IdResponse response = await Api.IssuingClient().RemoveCardControl(_cardControl.Id);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(_cardControl.Id);
        }
    }
}