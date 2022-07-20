namespace Checkout.Payments
{
    public class BillingDescriptor
    {
        public string Name { get; set; }

        public string City { get; set; }

        //Not available on Previous

        public string Reference { get; set; }
    }
}