using Checkout.Common;

namespace Checkout.Issuing.Cards.Responses.Enrollment
{
    public class ThreeDSEnrollmentDetailsResponse : Resource
    {
        public string Locale { get; set; }

        public Phone PhoneNumber { get; set; }

        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }
    }
}