using System;

namespace Checkout.Sessions
{
    public class MerchantRiskInfo
    {
        public string DeliveryEmail { get; set; }

        public DeliveryTimeframe? DeliveryTimeframe { get; set; }

        public bool? IsPreorder { get; set; }

        public bool? IsReorder { get; set; }

        public ShippingIndicator? ShippingIndicator { get; set; }
        
        public ReorderItemsIndicatorType? ReorderItemsIndicator { get; set; }
        
        public PreOrderPurchaseIndicatorType? PreOrderPurchaseIndicator { get; set; }
        
        public DateTime? PreOrderDate { get; set; }
        
        public string GiftCardAmount { get; set; }
        
        public string GiftCardCurrency { get; set; }
        
        public string GiftCardCount { get; set; }
        
    }
}