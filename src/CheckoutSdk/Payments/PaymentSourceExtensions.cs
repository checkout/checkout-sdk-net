namespace Checkout.Payments
{
    public static class SourceResponseExtensions
    {
        public static CardSourceResponse AsCardSource(this IResponsePaymentSource response)
        {
            return response as CardSourceResponse;
        }
    }
}
