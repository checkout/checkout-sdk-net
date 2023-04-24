namespace Checkout.Issuing.Cards.Enroll
{
    public class CardEnrollThreeDSSecurityQuestionRequest : CardEnrollThreeDSRequest
    {
        public SecurityPair SecurityPair { get; set; }
    }
}