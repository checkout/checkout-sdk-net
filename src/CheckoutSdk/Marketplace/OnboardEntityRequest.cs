namespace Checkout.Marketplace
{
    public class OnboardEntityRequest
    {
        public string Reference { get; set; }

        public ContactDetails ContactDetails { get; set; }

        public Profile Profile { get; set; }

        public Company Company { get; set; }

        public Individual Individual { get; set; }
    }
}