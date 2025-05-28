namespace Checkout.Payments.Request
{
    public class Provider
    {
        /// <summary> The unique identifier for the device (Optional) </summary>
        public string Id { get; set; }

        /// <summary> The name of the provider that generated the device identifier (Optional) </summary>
        public string Name { get; set; }

    }
}