namespace Checkout.Sdk.Payments
{
    public class BillingDescriptor
    {
        public BillingDescriptor(string name, string city)
        {
            Name = name;
            City = city;
        }

        public string Name { get; set; }
        public string City { get; set; }
    }
}