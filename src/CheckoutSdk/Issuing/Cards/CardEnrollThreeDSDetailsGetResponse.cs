using Checkout.Common;

namespace Checkout.Issuing.Cards
{
    public class CardEnrollThreeDSDetailsGetResponse : Resource
    {
        public string Locale { get; set; }

        public Phone PhoneNumber { get; set; }

        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }
    }
}