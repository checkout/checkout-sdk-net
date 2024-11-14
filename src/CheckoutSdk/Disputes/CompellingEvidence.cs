using System;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class CompellingEvidence
    {
        public string MerchandiseOrService { get; set; }
        
        public string MerchandiseOrServiceDesc { get; set; }
        
        public DateTime? MerchandiseOrServiceProvidedDate { get; set; }
        
        public ShippingDeliveryStatusType? ShippingDeliveryStatus { get; set; }
        
        public TrackingInformationType? TrackingInformation { get; set; }
        
        public string UserId { get; set; }
        
        public string IpAddress { get; set; }
        
        public ShippingAddress ShippingAddress { get; set; }
        
        public IList<HistoricalTransactions> HistoricalTransactions { get; set; }
    }
}