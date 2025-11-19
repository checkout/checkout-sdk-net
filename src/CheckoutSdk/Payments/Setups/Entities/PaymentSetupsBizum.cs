using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsBizum
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public PaymentSetupsBizumOptions PaymentMethodOptions { get; set; }
    }

    public class PaymentSetupsBizumOptions
    {
        public PaymentSetupsBizumPayNow PayNow { get; set; }
    }

    public class PaymentSetupsBizumPayNow
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }
    }
}