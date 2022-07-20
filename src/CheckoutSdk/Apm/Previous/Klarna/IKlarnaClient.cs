using Checkout.Payments;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Previous.Klarna
{
    public interface IKlarnaClient
    {
        Task<CreditSessionResponse> CreateCreditSession(
            CreditSessionRequest creditSessionRequest,
            CancellationToken cancellationToken = default);

        Task<CreditSession> GetCreditSession(
            string sessionId,
            CancellationToken cancellationToken = default);

        Task<CaptureResponse> CapturePayment(
            string paymentId,
            OrderCaptureRequest orderCaptureRequest,
            CancellationToken cancellationToken = default);

        Task<VoidResponse> VoidPayment(
            string paymentId,
            VoidRequest voidRequest,
            CancellationToken cancellationToken = default);
    }
}