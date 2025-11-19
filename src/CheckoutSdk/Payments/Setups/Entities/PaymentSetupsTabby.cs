using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsTabby
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public PaymentSetupsTabbyOptions PaymentMethodOptions { get; set; }
    }

    public class PaymentSetupsTabbyOptions
    {
        public PaymentSetupsTabbyInstallments Installments { get; set; }
    }

    public class PaymentSetupsTabbyInstallments
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }
    }
}