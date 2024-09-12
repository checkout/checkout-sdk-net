namespace Checkout.Accounts
{
    public class Capabilities
    {
        public PaymentsNC Payments { get; set; }

        public PayoutsNC Payouts { get; set; }

        public IssuingCapabilities Issuing { get; set; }

        public class PaymentsNC
        {
            public bool? Available { get; set; }

            public bool? Enabled { get; set; }
        }

        public class PayoutsNC
        {
            public bool? Available { get; set; }

            public bool? Enabled { get; set; }
        }

        public class IssuingCapabilities
        {
            public bool? Available { get; set; }

            public bool? Enabled { get; set; }
        }
    }
}