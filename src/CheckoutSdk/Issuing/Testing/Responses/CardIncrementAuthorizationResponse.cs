namespace Checkout.Issuing.Testing.Responses
{
    public class CardIncrementAuthorizationResponse : HttpMetadata
    {
        public TransactionStatus? Status { get; set; }
    }
}