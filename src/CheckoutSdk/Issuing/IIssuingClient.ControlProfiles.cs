using Checkout.Common;
using Checkout.Issuing.ControlProfiles.Requests;
using Checkout.Issuing.ControlProfiles.Responses;
using Checkout.Payments;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        Task<ControlProfileResponse> CreateControlProfile(ControlProfileRequest controlProfileRequest,
            CancellationToken cancellationToken = default);

        Task<ControlProfilesResponse> GetAllControlProfiles(string targetId,
            CancellationToken cancellationToken = default);

        Task<ControlProfileResponse> GetControlProfileDetails(string controlProfileId,
            CancellationToken cancellationToken = default);

        Task<ControlProfileResponse> UpdateControlProfile(string controlProfileId,
            ControlProfileRequest controlProfileRequest,
            CancellationToken cancellationToken = default);

        Task<VoidResponse> RemoveControlProfile(string controlProfileId,
            CancellationToken cancellationToken = default);

        Task<Resource> AddTargetToControlProfile(string controlProfileId,
            string targetId,
            CancellationToken cancellationToken = default);

        Task<Resource> RemoveTargetFromControlProfile(string controlProfileId,
            string targetId,
            CancellationToken cancellationToken = default);
    }
}