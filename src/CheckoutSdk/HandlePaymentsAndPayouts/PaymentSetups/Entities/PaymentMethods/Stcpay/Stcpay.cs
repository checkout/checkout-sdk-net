using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Stcpay
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public string Otp { get; set; }

        public StcpayOptions PaymentMethodOptions { get; set; }
    }
}