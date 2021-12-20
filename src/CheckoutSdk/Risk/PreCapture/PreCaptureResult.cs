namespace Checkout.Risk.PreCapture
{
    public class PreCaptureResult
    {
        public PreCaptureDecision? Decision { get; set; }

        public string Details { get; set; }
    }
}