namespace Checkout.Risk.PreCapture
{
    public sealed class AuthorizationResult
    {
        public string AvsCode { get; set; }

        public string CvvResult { get; set; }
    }
}