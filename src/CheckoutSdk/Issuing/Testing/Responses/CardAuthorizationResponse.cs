namespace Checkout.Issuing.Testing.Responses
{
    public class CardAuthorizationResponse : HttpMetadata
    {
        public string Id { get; set; }

        public TransactionStatus? Status { get; set; }
    }
}