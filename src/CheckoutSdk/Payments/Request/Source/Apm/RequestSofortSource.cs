using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>Sofort was deprecated as a payment source type on 2024/12/03.</summary>
    [System.Obsolete("Sofort was deprecated as a payment source type on 2024/12/03.")]
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