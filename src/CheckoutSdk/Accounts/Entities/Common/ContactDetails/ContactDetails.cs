using Checkout.Common;

namespace Checkout.Accounts.Entities.Common.ContactDetails
{
    public class ContactDetails
    {
        // Common
        
        public Invitee Invitee { get; set; }
        
        // GB Company Full (3.0)
        public Phone Phone { get; set; }
        
        public EmailAddresses EmailAddresses { get; set; }

    }
}