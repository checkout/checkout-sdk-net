using Checkout.Common;

namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public abstract class AbstractThreeDsEnrollmentRequest
    {
        public string Locale { get; set; }

        public Phone PhoneNumber { get; set; }
    }
}