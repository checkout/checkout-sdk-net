using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsBizumPayNow
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }
    }
}