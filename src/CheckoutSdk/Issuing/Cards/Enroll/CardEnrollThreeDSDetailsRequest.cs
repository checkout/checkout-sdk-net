namespace Checkout.Issuing.Cards.Enroll
{
    public class CardEnrollThreeDSDetailsRequest : CardEnrollThreeDSRequest
    {
        public SecurityPair SecurityPair { get; set; }

        public string Password { get; set; }
    }
}