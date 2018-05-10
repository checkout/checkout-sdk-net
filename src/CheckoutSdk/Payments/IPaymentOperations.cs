using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentOperations
    {
        Task<PaymentResponse<PaymentSource>> GetPaymentAsync(string paymentToken);
        Task<CardPaymentResponse> RequestAsync(CardPaymentRequest cardPaymentRequest);
        Task<CardPaymentResponse> RequestAsync(TokenPaymentRequest tokenPaymentRequest);
        Task<AlternativePaymentResponse> RequestAsync<TRequest>(TRequest alternativePaymentRequest);
    }
}