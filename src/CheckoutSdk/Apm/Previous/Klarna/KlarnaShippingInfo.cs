namespace Checkout.Apm.Previous.Klarna
{
    public class KlarnaShippingInfo
    {
        public string ShippingCompany { get; set; }

        public string ShippingMethod { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingUri { get; set; }

        public string ReturnShippingCompany { get; set; }

        public string ReturnTrackingNumber { get; set; }

        public string ReturnTrackingUri { get; set; }
    }
}