using Checkout.Common;
using Checkout.Issuing.ControlGroups.Requests;
using Checkout.Issuing.ControlGroups.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<ControlGroupResponse> CreateControlGroup(
            CreateControlGroupRequest createControlGroupRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createControlGroupRequest", createControlGroupRequest);
            return ApiClient.Post<ControlGroupResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlGroupsPath),
                SdkAuthorization(),
                createControlGroupRequest,
                cancellationToken
            );
        }

        public Task<ControlGroupsResponse> GetTargetControlGroups(
            ControlGroupQueryTarget query,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("query", query);
            return ApiClient.Query<ControlGroupsResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlGroupsPath),
                SdkAuthorization(),
                query,
                cancellationToken
            );
        }

        public Task<ControlGroupResponse> GetControlGroupDetails(
            string controlGroupId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlGroupId", controlGroupId);
            return ApiClient.Get<ControlGroupResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlGroupsPath, controlGroupId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<IdResponse> RemoveControlGroup(
            string controlGroupId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlGroupId", controlGroupId);
            return ApiClient.Delete<IdResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlGroupsPath, controlGroupId),
                SdkAuthorization(),
                cancellationToken
            );
        }
    }
}