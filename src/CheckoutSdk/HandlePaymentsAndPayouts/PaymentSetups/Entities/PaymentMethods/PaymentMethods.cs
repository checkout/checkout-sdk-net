using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethods
    {
        public Klarna Klarna { get; set; }

        public Stcpay Stcpay { get; set; }

        public Tabby Tabby { get; set; }

        public Bizum Bizum { get; set; }
    }
}