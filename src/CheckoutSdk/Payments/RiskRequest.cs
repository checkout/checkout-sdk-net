namespace Checkout.Payments
{
    public class RiskRequest
    {
        public bool? Enabled { get; set; }

        public string DeviceSessionId { get; set; }
    }
}