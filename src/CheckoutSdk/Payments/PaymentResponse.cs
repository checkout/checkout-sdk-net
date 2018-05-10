
namespace Checkout.Payments
{
    public class PaymentResponse<TSource> : ApiResponse, IPaymentResponse
    {
        public bool Approved {get;set;}
        public TSource Source { get; }

        public AcceptedPayment AccepterPayment { get; }
        public RequestedPayment RequestedPayment { get; }
    }
}