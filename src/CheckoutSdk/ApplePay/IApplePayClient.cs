using Checkout.ApplePay.Requests;
using Checkout.ApplePay.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.ApplePay
{
    public interface IApplePayClient
    {
        /// <summary>
        /// Upload a payment processing certificate. This will allow you to start processing payments via Apple Pay.
        /// </summary>
        /// <param name="request">The upload certificate request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<UploadCertificateResponse> UploadPaymentProcessingCertificate(UploadCertificateRequest request,
                                                                CancellationToken cancellationToken = default);

        /// <summary>
        /// Enroll a domain to the Apple Pay Service
        /// </summary>
        /// <param name="request">The domain enrollment request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<EmptyResponse> EnrollDomain(EnrollDomainRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generate a certificate signing request. You'll need to upload this to your Apple Developer account to download a payment processing certificate.
        /// </summary>
        /// <param name="request">The certificate signing request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<GenerateSigningRequestResponse> GenerateCertificateSigningRequest(GenerateSigningRequestRequest request,
                                                                CancellationToken cancellationToken = default);
    }
}