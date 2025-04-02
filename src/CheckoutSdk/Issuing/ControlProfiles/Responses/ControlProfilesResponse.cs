using System.Collections.Generic;

namespace Checkout.Issuing.ControlProfiles.Responses
{
    public class ControlProfilesResponse : HttpMetadata
    {
        public IList<ControlProfileResponse> ControlProfiles { get; set; }
    }
}