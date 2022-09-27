namespace Checkout.Payments.Response
{
    public class ProviderAuthorizedPaymentMethod
    {
        public string Type { get; set; }

        public string Description { get; set; }

        public long? NumberOfInstallments { get; set; }

        public long? NumberOfDays { get; set; }
    }
}