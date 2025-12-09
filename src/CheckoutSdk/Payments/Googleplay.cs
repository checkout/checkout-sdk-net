using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;

namespace Checkout.Payments
{
    public class Googlepay
    {
        public AccountHolder AccountHolder { get; set; }
        
        public StorePaymentDetailsType? StorePaymentDetails { get; set; }
    }
}