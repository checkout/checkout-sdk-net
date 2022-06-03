namespace Checkout.Sessions
{
    public class MerchantRiskInfo
    {
        public string DeliveryEmail { get; set; }

        public DeliveryTimeframe? DeliveryTimeframe { get; set; }

        public bool? IsPreorder { get; set; }

        public bool? IsReorder { get; set; }

        public ShippingIndicator? ShippingIndicator { get; set; }
    }
}