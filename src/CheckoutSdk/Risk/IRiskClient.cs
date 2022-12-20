using Checkout.Risk.PreAuthentication;
using Checkout.Risk.PreCapture;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Risk
{
    [Obsolete("Risk endpoints are no longer supported officially, This module will be removed in a future release.", false)]
    public interface IRiskClient
    {
        Task<PreAuthenticationAssessmentResponse> RequestPreAuthenticationRiskScan(
            PreAuthenticationAssessmentRequest preAuthenticationAssessmentRequest,
            CancellationToken cancellationToken = default);

        Task<PreCaptureAssessmentResponse> RequestPreCaptureRiskScan(
            PreCaptureAssessmentRequest preCaptureAssessmentRequest, CancellationToken cancellationToken = default);
    }
}