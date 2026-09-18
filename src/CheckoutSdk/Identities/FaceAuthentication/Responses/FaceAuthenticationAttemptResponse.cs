using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.FaceAuthentication.Responses
{
    /// <summary>
    /// Response for a face authentication attempt
    /// </summary>
    public class FaceAuthenticationAttemptResponse : BaseAttemptWithSessionResponse<FaceAuthenticationAttemptStatus>
    {
        /// <summary>
        /// The applicant's details
        /// [Optional]
        /// </summary>
        public ClientInformation ClientInformation { get; set; }
    }
}
