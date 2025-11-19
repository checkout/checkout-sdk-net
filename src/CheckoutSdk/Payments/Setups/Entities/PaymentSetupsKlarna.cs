using System.Collections.Generic;
using Checkout.Common;

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

    public class PaymentSetupsAccountHolder
    {
        public Address BillingAddress { get; set; }
    }

    public class PaymentSetupsKlarnaOptions
    {
        public PaymentSetupsKlarnaSDK SDK { get; set; }
    }

    public class PaymentSetupsKlarnaSDK
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public PaymentSetupsKlarnaAction Action { get; set; }
    }

    public class PaymentSetupsKlarnaAction
    {
        public string Type { get; set; }

        public string ClientToken { get; set; }

        public string SessionId { get; set; }
    }
}