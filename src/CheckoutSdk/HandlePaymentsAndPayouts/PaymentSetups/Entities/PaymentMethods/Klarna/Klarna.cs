using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Klarna
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public KlarnaAccountHolder AccountHolder { get; set; }

        public KlarnaPaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}