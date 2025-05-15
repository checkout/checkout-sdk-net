using Checkout.Common;

namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class SecurityQuestionThreeDsUpdateRequest : AbstractThreeDsEnrollmentRequest
    {
        public SecurityPair SecurityPair { get; set; }
    }
}