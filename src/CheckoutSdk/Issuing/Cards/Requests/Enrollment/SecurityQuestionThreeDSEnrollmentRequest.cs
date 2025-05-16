namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class SecurityQuestionThreeDsEnrollmentRequest : AbstractThreeDsEnrollmentRequest
    {
        public SecurityPair SecurityPair { get; set; }
    }
}