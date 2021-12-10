using Checkout.Risk.PreAuthentication;
using Checkout.Risk.PreCapture;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Risk
{
    public class RiskClient : AbstractClient, IRiskClient
    {
        private const string RiskAssessmentsPath = "risk/assessments";

        public RiskClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient, configuration,
            SdkAuthorizationType.SecretKey)
        {
        }

        public Task<PreAuthenticationAssessmentResponse> RequestPreAuthenticationRiskScan(
            PreAuthenticationAssessmentRequest preAuthenticationAssessmentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("preAuthenticationAssessmentRequest", preAuthenticationAssessmentRequest);
            return ApiClient.Post<PreAuthenticationAssessmentResponse>(
                BuildPath(RiskAssessmentsPath, "pre-authentication"), SdkAuthorization(),
                preAuthenticationAssessmentRequest, cancellationToken);
        }

        public Task<PreCaptureAssessmentResponse> RequestPreCaptureRiskScan(
            PreCaptureAssessmentRequest preCaptureAssessmentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("preCaptureAssessmentRequest", preCaptureAssessmentRequest);
            return ApiClient.Post<PreCaptureAssessmentResponse>(BuildPath(RiskAssessmentsPath, "pre-capture"),
                SdkAuthorization(), preCaptureAssessmentRequest, cancellationToken);
        }
    }
}