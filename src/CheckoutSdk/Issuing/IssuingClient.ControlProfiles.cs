using Checkout.Common;
using Checkout.Issuing.ControlProfiles.Requests;
using Checkout.Issuing.ControlProfiles.Responses;
using Checkout.Payments;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<ControlProfileResponse> CreateControlProfile(ControlProfileRequest controlProfileRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlProfileRequest", controlProfileRequest);
            return ApiClient.Post<ControlProfileResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath),
                SdkAuthorization(),
                controlProfileRequest,
                cancellationToken
            );
        }

        public Task<ControlProfilesResponse> GetAllControlProfiles(string targetId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("targetId", targetId);
            return ApiClient.Get<ControlProfilesResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<ControlProfileResponse> GetControlProfileDetails(string controlProfileId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlProfileId", controlProfileId);
            return ApiClient.Get<ControlProfileResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath, controlProfileId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<ControlProfileResponse> UpdateControlProfile(string controlProfileId, ControlProfileRequest controlProfileRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlProfileId", controlProfileId, "controlProfileRequest", controlProfileRequest);
            return ApiClient.Patch<ControlProfileResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath, controlProfileId),
                SdkAuthorization(),
                controlProfileRequest,
                cancellationToken
            );
        }

        public Task<VoidResponse> RemoveControlProfile(string controlProfileId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlProfileId", controlProfileId);
            return ApiClient.Delete<VoidResponse>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath, controlProfileId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<Resource> AddTargetToControlProfile(string controlProfileId, string targetId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlProfileId", controlProfileId, "targetId", targetId);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath, controlProfileId, AddPath, targetId),
                SdkAuthorization(),
                null,
                cancellationToken,
                null
            );
        }

        public Task<Resource> RemoveTargetFromControlProfile(string controlProfileId, string targetId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlProfileId", controlProfileId, "targetId", targetId);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, ControlsPath, ControlProfilesPath, controlProfileId, RemovePath, targetId),
                SdkAuthorization(),
                null,
                cancellationToken,
                null
            );
        }
    }
}