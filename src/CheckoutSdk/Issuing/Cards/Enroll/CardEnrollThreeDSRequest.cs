using Checkout.Common;

namespace Checkout.Issuing.Cards.Enroll
{
    public abstract class CardEnrollThreeDSRequest
    {
        public string Locale { get; set; }

        public Phone PhoneNumber { get; set; }
    }
}