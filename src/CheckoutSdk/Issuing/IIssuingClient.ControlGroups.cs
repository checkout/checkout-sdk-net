using Checkout.Common;
using Checkout.Issuing.ControlGroups.Requests;
using Checkout.Issuing.ControlGroups.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        Task<ControlGroupResponse> CreateControlGroup(
            CreateControlGroupRequest createControlGroupRequest,
            CancellationToken cancellationToken = default);

        Task<ControlGroupsResponse> GetTargetControlGroups(
            ControlGroupQueryTarget query,
            CancellationToken cancellationToken = default);

        Task<ControlGroupResponse> GetControlGroupDetails(
            string controlGroupId,
            CancellationToken cancellationToken = default);

        Task<IdResponse> RemoveControlGroup(
            string controlGroupId,
            CancellationToken cancellationToken = default);
    }
}