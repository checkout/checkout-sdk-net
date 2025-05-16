using Checkout.Common;
using Checkout.Issuing.Cardholders.Responses;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Common;
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
        private AbstractCardCreateResponse _cardRequest;
        private AbstractCardControlResponse _abstractCardControl;

        public async Task InitializeAsync()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            AbstractCardCreateRequest abstractCardCreateRequest = await CreateVirtualCard(cardholderResponse.Id);
            _cardRequest = await Api.IssuingClient().CreateCard(abstractCardCreateRequest);

            await Api.IssuingClient().ActivateCard(_cardRequest.Id);

            AbstractCardControlRequest abstractCardControlRequest = new VelocityCardControlRequest
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

            _abstractCardControl = await Api.IssuingClient().CreateCardControl(abstractCardControlRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private void ShouldCreateCardControl()
        {
            AbstractCardControlResponse response = _abstractCardControl;

            response.ShouldNotBeNull();
            response.TargetId.ShouldBe(_cardRequest.Id);
            response.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCardControls()
        {
            CardControlQueryTarget cardControlQueryTarget = new CardControlQueryTarget { TargetId = _cardRequest.Id };

            CardControlsQueryResponse response = await Api.IssuingClient().GetCardControls(cardControlQueryTarget);

            response.ShouldNotBeNull();
            response.Controls.ShouldNotBeNull();
            foreach (AbstractCardControlResponse control in response.Controls)
            {
                control.TargetId.ShouldBe(_cardRequest.Id);
            }
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetControlDetails()
        {
            AbstractCardControlResponse response = await Api.IssuingClient().GetCardControlDetails(_abstractCardControl.Id);

            response.ShouldNotBeNull();
            response.TargetId.ShouldBe(_cardRequest.Id);
            response.Id.ShouldBe(_abstractCardControl.Id);
            response.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldUpdateControl()
        {
            const string description = "New max spend of 1000€ per month for restaurants";
            AbstractCardControlUpdate cardControlUpdate = new VelocityCardControlUpdate
            {
                Description = description,
                VelocityLimit = new VelocityLimit
                {
                    AmountLimit = 1000,
                    VelocityWindow = new VelocityWindow { Type = VelocityWindowType.Monthly }
                },
            };

            AbstractCardControlResponse response =
                await Api.IssuingClient().UpdateCardControl(_abstractCardControl.Id, cardControlUpdate);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(_abstractCardControl.Id);
            response.Description.ShouldBe(description);
            response.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldRemoveControl()
        {
            IdResponse response = await Api.IssuingClient().RemoveCardControl(_abstractCardControl.Id);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(_abstractCardControl.Id);
        }
    }
}