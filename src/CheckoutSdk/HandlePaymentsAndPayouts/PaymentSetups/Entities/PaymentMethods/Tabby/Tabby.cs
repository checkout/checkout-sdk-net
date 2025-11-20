using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Tabby
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public TabbyOptions PaymentMethodOptions { get; set; }
    }
}