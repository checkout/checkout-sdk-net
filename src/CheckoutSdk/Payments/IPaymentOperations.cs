using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentOperations
    {
        Task<ApiResponse<CardPaymentResponse>> RequestAsync(CardPaymentRequest cardPaymentRequest);
        Task<ApiResponse<CardPaymentResponse>> RequestAsync(TokenPaymentRequest tokenPaymentRequest);
        Task<ApiResponse<AlternativePaymentResponse>> RequestAsync<TRequest>(TRequest alternativePaymentRequest);
    }
}