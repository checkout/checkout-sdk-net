using Checkout.Payments.Response.Source;
using Checkout.Payments.Util;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsResponse : PaymentContexts
    {
        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]
        public IResponseSource Source { get; set; }
    }
}