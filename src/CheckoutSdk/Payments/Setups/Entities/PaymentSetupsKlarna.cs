using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsKlarna
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public PaymentSetupsAccountHolder AccountHolder { get; set; }

        public PaymentSetupsKlarnaOptions PaymentMethodOptions { get; set; }
    }
}