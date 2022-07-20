using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestSofortSource : AbstractRequestSource
    {
        [JsonProperty("countryCode")]
        public CountryCode? CountryCode { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
        
        public RequestSofortSource() : base(PaymentSourceType.Sofort)
        {
        }
    }
}