namespace Checkout.Payments
{
    public class ProcessingSettings 
    {
        public bool? Aft { get; set; }
        
        public long? TaxAmount { get; set; }
        
        public long? ShippingAmount { get; set; }
            
    }
}