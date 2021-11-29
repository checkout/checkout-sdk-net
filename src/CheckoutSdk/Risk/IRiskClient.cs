using System.Threading;
using System.Threading.Tasks;
using Checkout.Risk.PreAuthentication;
using Checkout.Risk.PreCapture;

namespace Checkout.Risk
{
    public interface IRiskClient
    {
        Task<PreAuthenticationAssessmentResponse> RequestPreAuthenticationRiskScan(
            PreAuthenticationAssessmentRequest preAuthenticationAssessmentRequest,
            CancellationToken cancellationToken = default);

        Task<PreCaptureAssessmentResponse> RequestPreCaptureRiskScan(
            PreCaptureAssessmentRequest preCaptureAssessmentRequest, CancellationToken cancellationToken = default);
    }
}