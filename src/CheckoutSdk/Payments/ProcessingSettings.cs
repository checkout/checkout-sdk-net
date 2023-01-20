using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class ProcessingSettings
    {
        public string OrderId { get; set; }

        public long? TaxAmount { get; set; }

        public long? DiscountAmount { get; set; }

        public long? DutyAmount { get; set; }

        public long? ShippingAmount { get; set; }

        public long? ShippingTaxAmount { get; set; }

        public bool? Aft { get; set; }

        public PreferredSchema? PreferredScheme { get; set; }

        public MerchantInitiatedReason? MerchantInitiatedReason { get; set; }

        public long? CampaignId { get; set; }

        public ProductType? ProductType { get; set; }

        public string OpenId { get; set; }

        public long? OriginalOrderAmount { get; set; }

        public string ReceiptId { get; set; }

        public TerminalType? TerminalType { get; set; }

        public OsType? OsType { get; set; }

        public string InvoiceId { get; set; }

        public string BrandName { get; set; }

        public string Locale { get; set; }

        public ShippingPreference? ShippingPreference { get; set; }

        public UserAction? UserAction { get; set; }

        public IList<IDictionary<string, string>> SetTransactionContext { get; set; }

        public IList<AirlineData> AirlineData { get; set; }

        public DLocalProcessingSettings Dlocal { get; set; }

        public string OtpValue { get; set; }

        public CountryCode? PurchaseCountry { get; set; }

        public IList<string> CustomPaymentMethodIds { get; set; }
        
        public string MerchantCallbackUrl { get; set; }

        public long? ShippingDelay { get; set; }

        public IList<ShippingInfo> ShippingInfo { get; set; }
        
        [JsonProperty(PropertyName = "senderInformation")]
        public string SenderInformation { get; set; }
    }
}