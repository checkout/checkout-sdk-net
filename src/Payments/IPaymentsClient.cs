using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        Task<ApiResponse<CardPaymentResponse>> RequestAsync(CardPaymentRequest cardPaymentRequest);
    }
}