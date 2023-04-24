namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class PasswordThreeDSEnrollmentRequest : ThreeDSEnrollmentRequest
    {
        public string Password { get; set; }
    }
}