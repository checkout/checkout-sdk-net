namespace Checkout.Issuing.Testing.Requests
{
    public class CardAuthorizationRequest
    {
        public CardSimulation Card { get; set; }

        public TransactionSimulation Transaction { get; set; }
    }
}