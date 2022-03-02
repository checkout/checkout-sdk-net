namespace Checkout.Payments
{
    public class BillingDescriptor
    {
        public string Name { get; set; }

        public string City { get; set; }

        // Only available in Four

        public string Reference { get; set; }
    }
}