using Checkout.Common;

namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class ThreeDSUpdateRequest
    {
        public SecurityPair SecurityPair { get; set; }

        public string Password { get; set; }

        public string Locale { get; set; }

        public Phone PhoneNumber { get; set; }
    }
}