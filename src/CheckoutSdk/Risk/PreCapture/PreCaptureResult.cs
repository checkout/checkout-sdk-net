namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureResult
    {
        public PreCaptureDecision? Decision { get; set; }

        public string Details { get; set; }
    }
}