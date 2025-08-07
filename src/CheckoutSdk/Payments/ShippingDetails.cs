using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Common;
using Checkout.Payments.Contexts;


namespace Checkout.Payments
{
    public class ShippingDetails
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public Address Address { get; set; }

        public Phone Phone { get; set; }

        public string FromAddressZip { get; set; }

        public DeliveryTimeframeType? Timeframe { get; set; }

        public PaymentContextsShippingMethod? Method { get; set; }

        public int? Delay { get; set; }
    }
}