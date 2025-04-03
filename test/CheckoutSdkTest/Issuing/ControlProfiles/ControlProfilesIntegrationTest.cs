using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.ControlProfiles.Requests;
using Checkout.Issuing.ControlProfiles.Responses;
using Checkout.Payments;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.ControlProfiles
{
    public class ControlProfilesIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private ControlProfileResponse _controlProfile;
        
        public async Task InitializeAsync()
        {
            ControlProfileRequest request = new ControlProfileRequest { Name = "Test Control Profile" };
            _controlProfile = await Api.IssuingClient().CreateControlProfile(request);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Prevent limit exceeded")]
        private Task ShouldCreateControlProfile()
        {
            _controlProfile.ShouldNotBeNull();
            _controlProfile.Id.ShouldNotBeNull();
            _controlProfile.Name.ShouldBe("Test Control Profile");
            _controlProfile.CreatedDate.ShouldNotBeNull();
            _controlProfile.LastModifiedDate.ShouldNotBeNull();
            _controlProfile.Links.ShouldNotBeNull();
            return Task.CompletedTask;
        }

        [Fact(Skip = "Use on demand")]
        private async Task ShouldGetAllControlProfiles()
        {
            ControlProfilesResponse response = await Api.IssuingClient().GetAllControlProfiles(_controlProfile.Id);
            response.ShouldNotBeNull();
            response.ControlProfiles.ShouldNotBeNull();

            var controlProfiles = response.ControlProfiles.ToList();
            var profile = controlProfiles.Find(profile => profile.Id == _controlProfile.Id);
            profile.Name.ShouldBe("Test Control Profile");
            profile.CreatedDate.Value.ToShortDateString().ShouldBe(_controlProfile.CreatedDate.Value.ToShortDateString());
            profile.LastModifiedDate.Value.ToShortDateString().ShouldBe(_controlProfile.LastModifiedDate.Value.ToShortDateString());
        }

        [Fact(Skip = "Use on demand")]
        private async Task ShouldGetControlProfile()
        {
            ControlProfileResponse response = await Api.IssuingClient().GetControlProfileDetails(_controlProfile.Id);
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Name.ShouldBe("Test Control Profile");
            response.CreatedDate.Value.ToShortDateString().ShouldBe(_controlProfile.CreatedDate.Value.ToShortDateString());
            response.LastModifiedDate.Value.ToShortDateString().ShouldBe(_controlProfile.LastModifiedDate.Value.ToShortDateString());
        }

        [Fact(Skip = "Use on demand")]
        private async Task ShouldUpdateControlProfile()
        {
            ControlProfileRequest request = new ControlProfileRequest { Name = "Updated Control Profile" };
            ControlProfileResponse response =
                await Api.IssuingClient().UpdateControlProfile(_controlProfile.Id, request);
            response.ShouldNotBeNull();
            response.LastModifiedDate.Value.ToUniversalTime().ShouldBeGreaterThan(_controlProfile.LastModifiedDate.Value.ToUniversalTime());
            
            ControlProfileResponse controlProfile = await Api.IssuingClient().GetControlProfileDetails(_controlProfile.Id);
            controlProfile.ShouldNotBeNull();
            controlProfile.Id.ShouldNotBeNull();
            controlProfile.Name.ShouldBe("Updated Control Profile");
            controlProfile.CreatedDate.Value.ToShortDateString().ShouldBe(_controlProfile.CreatedDate.Value.ToShortDateString());
            controlProfile.LastModifiedDate.Value.ToShortDateString().ShouldBe(response.LastModifiedDate.Value.ToShortDateString());
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldAddTargetToControlProfile()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest cardRequest = await CreateVirtualCard(cardholderResponse.Id);
            var card = await Api.IssuingClient().CreateCard(cardRequest);

            await Api.IssuingClient().ActivateCard(card.Id);
            Resource response =
                await Api.IssuingClient().AddTargetToControlProfile(_controlProfile.Id, card.Id);
            response.ShouldNotBeNull();
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldRemoveTargetFromControlProfile()
        {
            Resource response =
                await Api.IssuingClient().RemoveTargetFromControlProfile(_controlProfile.Id, "card_1");
            response.ShouldNotBeNull();
        }

        [Fact(Skip = "Use on demand")]
        private async Task ShouldRemoveControlProfile()
        {
            VoidResponse response = await Api.IssuingClient().RemoveControlProfile(_controlProfile.Id);
            response.ShouldNotBeNull();
        }
    }
}