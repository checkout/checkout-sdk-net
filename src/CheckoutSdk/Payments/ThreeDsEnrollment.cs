namespace Checkout.Payments.Response
{
    public class ThreeDsEnrollment
    {
        public bool Downgraded { get; set; }
        public string Enrolled { get; set; }
    }
}