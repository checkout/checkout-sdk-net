using Checkout.Common;

namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class PasswordThreeDsUpdateRequest : AbstractThreeDsEnrollmentRequest
    {
        public string Password { get; set; }
    }
}