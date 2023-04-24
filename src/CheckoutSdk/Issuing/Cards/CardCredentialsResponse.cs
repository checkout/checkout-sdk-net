namespace Checkout.Issuing.Cards
{
    public class CardCredentialsResponse : HttpMetadata
    {
        public string Number { get; set; }

        public string Cvc2 { get; set; }
    }
}