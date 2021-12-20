using Checkout.Common;

namespace Checkout.Risk.PreAuthentication
{
    public class PreAuthenticationAssessmentResponse : Resource
    {
        public string AssessmentId { get; set; }

        public long? Score { get; set; }

        public PreAuthenticationResult Result { get; set; }

        public PreAuthenticationWarning Warning { get; set; }
    }
}