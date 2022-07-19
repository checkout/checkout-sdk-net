using System.Collections.Generic;

namespace Checkout.Payments
{
    public class ProcessingSettings
    {
        public bool? Aft { get; set; }

        public MerchantInitiatedReason? MerchantInitiatedReason { get; set; }

        public DLocalProcessingSettings Dlocal { get; set; }

        public long? TaxAmount { get; set; }

        public long? ShippingAmount { get; set; }

        public PreferredSchema? PreferredScheme { get; set; }

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
    }
}