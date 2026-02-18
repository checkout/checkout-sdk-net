using Checkout.Common;
using Checkout.Issuing.Cardholders.Responses;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.ControlGroups.Common;
using Checkout.Issuing.ControlGroups.Requests;
using Checkout.Issuing.ControlGroups.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.ControlGroups
{
    public class ControlGroupsIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private CardholderResponse _cardholder;
        private AbstractCardCreateResponse _card;
        private ControlGroupResponse _controlGroup;

        public async Task InitializeAsync()
        {
            _cardholder = await CreateCardholder();
            var cardRequest = await CreateVirtualCard(_cardholder.Id);
            _card = await Api.IssuingClient().CreateCard(cardRequest);
            
            var controlGroupRequest = CreateValidControlGroupRequest(_card.Id);
            _controlGroup = await Api.IssuingClient().CreateControlGroup(controlGroupRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact (Skip = "Deactivated - needs investigation")]
        public async Task CreateControlGroup_ShouldReturnValidResponse()
        {
            // Arrange
            var request = CreateValidControlGroupRequest(_card.Id);

            // Act
            var response = await Api.IssuingClient().CreateControlGroup(request);

            // Assert
            AssertControlGroupCreated(response, request);
        }

        [Fact (Skip = "Deactivated - needs investigation")]
        public async Task GetTargetControlGroups_ShouldReturnValidResponse()
        {
            // Arrange
            var query = new ControlGroupQueryTarget { TargetId = _card.Id };

            // Act
            var response = await Api.IssuingClient().GetTargetControlGroups(query);

            // Assert
            AssertTargetControlGroupsRetrieved(response, _controlGroup.Id);
        }

        [Fact( Skip = "Deactivated - needs investigation")]
        public async Task GetControlGroupDetails_ShouldReturnValidResponse()
        {
            // Act
            var response = await Api.IssuingClient().GetControlGroupDetails(_controlGroup.Id);

            // Assert
            AssertControlGroupDetailsRetrieved(response, _controlGroup);
        }

        [Fact( Skip = "Deactivated - needs investigation")]
        public async Task RemoveControlGroup_ShouldReturnValidResponse()
        {
            // Arrange
            var request = CreateValidControlGroupRequest(_card.Id);
            var createResponse = await Api.IssuingClient().CreateControlGroup(request);

            // Act
            var response = await Api.IssuingClient().RemoveControlGroup(createResponse.Id);

            // Assert
            AssertControlGroupRemoved(response, createResponse.Id);
        }

        [Fact( Skip = "Deactivated - needs investigation")]
        public async Task ControlGroupFlow_ShouldWorkEndToEnd()
        {
            // Arrange - Create a new cardholder and card for this flow test
            var cardholder = await CreateCardholder();
            var cardRequest = await CreateVirtualCard(cardholder.Id);
            var card = await Api.IssuingClient().CreateCard(cardRequest);
            
            var createRequest = CreateValidControlGroupRequest(card.Id);

            // Act 1: Create control group
            var createResponse = await Api.IssuingClient().CreateControlGroup(createRequest);

            // Assert 1: Control group created successfully
            AssertControlGroupCreated(createResponse, createRequest);

            // Act 2: Get target control groups
            var targetQuery = new ControlGroupQueryTarget { TargetId = card.Id };
            var targetResponse = await Api.IssuingClient().GetTargetControlGroups(targetQuery);

            // Assert 2: Target control groups retrieved
            AssertTargetControlGroupsRetrieved(targetResponse, createResponse.Id);

            // Act 3: Get control group details
            var detailsResponse = await Api.IssuingClient().GetControlGroupDetails(createResponse.Id);

            // Assert 3: Details match created control group
            AssertControlGroupDetailsRetrieved(detailsResponse, createResponse);

            // Act 4: Remove control group
            var removeResponse = await Api.IssuingClient().RemoveControlGroup(createResponse.Id);

            // Assert 4: Control group removed successfully
            AssertControlGroupRemoved(removeResponse, createResponse.Id);

            // Act 5: Verify control group no longer exists in target
            var finalTargetResponse = await Api.IssuingClient().GetTargetControlGroups(targetQuery);

            // Assert 5: Control group no longer in target's control groups
            AssertControlGroupNotInTarget(finalTargetResponse, createResponse.Id);
        }

        private static CreateControlGroupRequest CreateValidControlGroupRequest(string targetId)
        {
            return new CreateControlGroupRequest
            {
                TargetId = targetId,
                FailIf = FailIfType.AllFail,
                Description = "Integration test control group",
                Controls = new List<ControlGroupControl>
                {
                    new MccControlGroupControl
                    {
                        Description = "Block grocery stores",
                        MccLimit = new Checkout.Issuing.Common.MccLimit
                        {
                            Type = Checkout.Issuing.Common.LimitControlType.Block,
                            MccList = new List<string> { "5411", "5422" }
                        }
                    },
                    new MidControlGroupControl
                    {
                        Description = "Allow specific merchant",
                        MidLimit = new Checkout.Issuing.Common.MidLimit
                        {
                            Type = Checkout.Issuing.Common.LimitControlType.Allow,
                            MidList = new List<string> { "1234567890" }
                        }
                    }
                }
            };
        }

        private static void AssertControlGroupCreated(ControlGroupResponse response, CreateControlGroupRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Id.ShouldStartWith("cgr_");
            response.TargetId.ShouldBe(request.TargetId);
            response.FailIf.ShouldBe(request.FailIf);
            response.Description.ShouldBe(request.Description);
            response.Controls.ShouldNotBeNull();
            response.Controls.Count.ShouldBe(request.Controls.Count);
            response.IsEditable.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
        }

        private static void AssertTargetControlGroupsRetrieved(ControlGroupsResponse response, string expectedControlGroupId)
        {
            response.ShouldNotBeNull();
            response.ControlGroups.ShouldNotBeNull();
            response.ControlGroups.Any(cg => cg.Id == expectedControlGroupId).ShouldBeTrue();
        }

        private static void AssertControlGroupDetailsRetrieved(ControlGroupResponse response, ControlGroupResponse expected)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldBe(expected.Id);
            response.TargetId.ShouldBe(expected.TargetId);
            response.FailIf.ShouldBe(expected.FailIf);
            response.Description.ShouldBe(expected.Description);
            response.IsEditable.ShouldBe(expected.IsEditable);
            response.CreatedDate.ShouldBe(expected.CreatedDate);
            response.LastModifiedDate.ShouldBe(expected.LastModifiedDate);
        }

        private static void AssertControlGroupRemoved(IdResponse response, string expectedId)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedId);
        }

        private static void AssertControlGroupNotInTarget(ControlGroupsResponse response, string controlGroupId)
        {
            response.ShouldNotBeNull();
            response.ControlGroups.ShouldNotBeNull();
            response.ControlGroups.Any(cg => cg.Id == controlGroupId).ShouldBeFalse();
        }
    }
}