namespace Checkout.Issuing.Cards.Requests.Enrollment
{
    public class PasswordThreeDsEnrollmentRequest : AbstractThreeDsEnrollmentRequest
    {
        public string Password { get; set; }
    }
}