namespace Checkout.Issuing.Testing.Responses
{
    public class CardReversalAuthorizationResponse : HttpMetadata
    {
        public ReversalStatus? Status { get; set; }
    }
}