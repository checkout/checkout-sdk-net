using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class KlarnaPaymentMethodOptions
    {
        public KlarnaOptionsSDK SDK { get; set; }
    }

    public class KlarnaOptionsSDK
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public IList<string> Flags { get; set; }

        public KlarnaAction Action { get; set; }
    }

    public class KlarnaAction
    {
        public string Type { get; set; }

        public string ClientToken { get; set; }

        public string SessionId { get; set; }
    }
}