namespace Checkout.Payments
{
    public class ProcessingSettings
    {
        public bool? Aft { get; set; }

        public DLocalProcessingSettings Dlocal { get; set; }

        public long? TaxAmount { get; set; }

        public long? ShippingAmount { get; set; }
        
        public PreferredSchema? PreferredScheme{ get; set; }
        
        public ProductType? ProductType{ get; set; }
        
        public string OpenId{ get; set; }
        
        public long? OriginalOrderAmount{ get; set; }
        
        public string ReceiptId{ get; set; }
        
    }
}