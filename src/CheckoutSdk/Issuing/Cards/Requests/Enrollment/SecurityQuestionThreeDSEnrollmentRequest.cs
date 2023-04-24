namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class SecurityQuestionThreeDSEnrollmentRequest : ThreeDSEnrollmentRequest
    {
        public SecurityPair SecurityPair { get; set; }
    }
}