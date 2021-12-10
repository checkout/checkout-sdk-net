using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class BillingInformation 
    {
        public Address Address { get; set; }

        public Phone Phone { get; set; }
       
    }
}