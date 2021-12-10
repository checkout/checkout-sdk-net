using System.Collections.Generic;

namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureWarning
    {
        public PreCaptureDecision? Decision { get; set; }

        public IList<string> Reasons { get; set; }
    }
}