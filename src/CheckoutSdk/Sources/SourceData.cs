namespace Checkout.Sources
{
    public class SourceData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AccountIban { get; set; }

        public string Bic { get; set; }

        public string BillingDescriptor { get; set; }

        public MandateType? MandateType { get; set; }
    }
}