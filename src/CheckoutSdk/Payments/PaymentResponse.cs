
namespace Checkout.Payments
{
    public class PaymentResponse<TSource> : IPaymentResponse
    {
        public bool Approved {get;set;}
        public TSource Source { get; }

        public string GetRedirectUrl()
        {
            return "";
        }
    }
}