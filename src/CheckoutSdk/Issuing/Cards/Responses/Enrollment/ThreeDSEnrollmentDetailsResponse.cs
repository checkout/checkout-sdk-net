using Checkout.Common;

namespace Checkout.Issuing.Cards.Responses.Enrollment
{
    public class ThreeDsEnrollmentDetailsResponse : Resource
    {
        public string Locale { get; set; }

        public Phone PhoneNumber { get; set; }

        public string CreatedDate { get; set; }
        
        public string LastModifiedDate { get; set; }
        
        public SecurityPair SecurityQuestion { get; set; }
        
        public string Password { get; set; }
    }
}