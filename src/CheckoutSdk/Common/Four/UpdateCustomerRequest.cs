namespace Checkout.Common.Four
{
    public sealed class UpdateCustomerRequest
    {
        public string Id { get; set; }

        public bool? Default { get; set; }
    }
}