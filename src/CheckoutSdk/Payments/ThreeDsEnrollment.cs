namespace Checkout.Payments
{
    public class ThreeDsEnrollment
    {
        public bool Downgraded { get; set; }
        public string Enrolled { get; set; }
        public string SignatureValid { get; set; }
        public string AuthenticationResponse { get; set; }
        public string Eci { get; set; }
        public string Cavv { get; set; }
        public string Xid { get; set; }
    }
}