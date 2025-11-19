using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsPaymentMethods
    {
        public PaymentSetupsKlarna Klarna { get; set; }

        public PaymentSetupsStcpay Stcpay { get; set; }

        public PaymentSetupsTabby Tabby { get; set; }

        public PaymentSetupsBizum Bizum { get; set; }
    }
}