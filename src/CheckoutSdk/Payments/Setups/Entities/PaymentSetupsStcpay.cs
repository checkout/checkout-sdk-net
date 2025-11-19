using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsStcpay
    {
        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public string Initialization { get; set; }

        public string Otp { get; set; }

        public PaymentSetupsStcpayOptions PaymentMethodOptions { get; set; }
    }

    public class PaymentSetupsStcpayOptions
    {
        public PaymentSetupsStcpayPayInFull PayInFull { get; set; }
    }

    public class PaymentSetupsStcpayPayInFull
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public PaymentSetupsStcpayAction Action { get; set; }
    }

    public class PaymentSetupsStcpayAction
    {
        public string Type { get; set; }
    }
}