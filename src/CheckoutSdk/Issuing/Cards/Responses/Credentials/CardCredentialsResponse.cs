namespace Checkout.Issuing.Cards.Responses.Credentials
{
    public class CardCredentialsResponse : HttpMetadata
    {
        public string Number { get; set; }

        public string Cvc2 { get; set; }
    }
}