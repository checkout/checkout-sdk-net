using Checkout.Issuing.Cards;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cardholders
{
    public class CardholdersIntegrationTest : IssuingCommon
    {
        [Fact]
        private async Task ShouldCreateCardholder()
        {
            CardholderRequest request = CardholderRequest();

            CardholderResponse response = await Api.IssuingClient().CreateCardholder(request);

            response.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(201);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Type.ShouldBe(CardholderType.Individual);
            response.Status.ShouldBe(CardholderStatus.Active);
            response.Reference.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldThrowErrorInvalidData()
        {
            CardholderRequest request = CardholderBadRequest();

            await AssertInvalidDataSent(Api.IssuingClient().CreateCardholder(request));
        }

        [Fact]
        private async Task ShouldGetCardholder()
        {
            CardholderRequest request = CardholderRequest();

            CardholderResponse cardholder = await Api.IssuingClient().CreateCardholder(request);

            CardholderDetailsResponse response = await Api.IssuingClient().GetCardholderDetails(cardholder.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldBe(cardholder.Id);
            response.Type.ShouldBe(cardholder.Type);
            response.FirstName.ShouldNotBeNull();
            response.MiddleName.ShouldNotBeNull();
            response.LastName.ShouldNotBeNull();
            response.Email.ShouldNotBeNull();
            response.PhoneNumber.ShouldNotBeNull();
            response.DateOfBirth.ShouldNotBeNull();
            response.BillingAddress.ShouldNotBeNull();
            response.ResidencyAddress.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.AccountEntityId.ShouldNotBeNull();
            response.EntityId.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldThrowErrorNotFoundCardholder()
        {
            await AssertNotFound(Api.IssuingClient().GetCardholderDetails("not_found"));
        }

        [Fact]
        private async Task ShouldGetCardholdersCards()
        {
            CardholderRequest request = CardholderRequest();

            CardholderResponse cardholder = await Api.IssuingClient().CreateCardholder(request);

            CardholderCardsResponse response = await Api.IssuingClient().GetCardholdersCards(cardholder.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Cards.ShouldNotBeNull();
            foreach (CardResponse card in response.Cards)
            {
                card.Id.ShouldBe(cardholder.Id);
            }
        }

        [Fact]
        private async Task ShouldThrowErrorNotFoundCardholderCards()
        {
            await AssertNotFound(Api.IssuingClient().GetCardholdersCards("not_found"));
        }
    }
}