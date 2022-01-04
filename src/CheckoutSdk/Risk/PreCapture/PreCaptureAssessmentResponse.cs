using Checkout.Common;

namespace Checkout.Risk.PreCapture
{
    public class PreCaptureAssessmentResponse : Resource
    {
        public string AssessmentId { get; set; }

        public int? Score { get; set; }

        public PreCaptureResult Result { get; set; }

        public PreCaptureWarning Warning { get; set; }
    }
}