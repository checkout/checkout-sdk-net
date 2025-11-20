using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class StcpayOptions
    {
        public StcpayPayInFull PayInFull { get; set; }
    }

    public class StcpayPayInFull
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public StcpayAction Action { get; set; }
    }

    public class StcpayAction
    {
        public string Type { get; set; }
    }
}