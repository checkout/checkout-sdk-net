using System.Collections.Generic;

namespace Checkout.Payments.Sessions
{
    public class PaymentMethods
    {
        public string Type { get; set; }
        
        public IList<string> CardSchemes { get; set; }
    }
}