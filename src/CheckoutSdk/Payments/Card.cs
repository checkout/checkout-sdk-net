using Checkout.Common;
using Checkout.Payments.Sessions;

namespace Checkout.Payments
{
    public class Card
    {
        public AccountHolder AccountHolder { get; set; }
        
        public StorePaymentDetailsType? StorePaymentDetails { get; set; }
    }
}