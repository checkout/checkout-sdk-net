namespace Checkout.Risk.PreCapture
{
    public class AuthenticationResult
    {
        public bool? Attempted { get; set; }

        public bool? Challenged { get; set; }

        public bool? Succeeded { get; set; }

        public bool? LiabilityShifted { get; set; }

        public string Method { get; set; }

        public string Version { get; set; }
    }
}