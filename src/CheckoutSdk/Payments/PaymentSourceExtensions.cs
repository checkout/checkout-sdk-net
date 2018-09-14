namespace Checkout.Payments
{
    public static class SourceResponseExtensions
    {
        public static TPaymentSourceResponse As<TPaymentSourceResponse>(this IResponsePaymentSource response)
            where TPaymentSourceResponse : class, IResponsePaymentSource
        {
            return response as TPaymentSourceResponse;
        }

        public static CardSourceResponse AsCardSourceResponse(this IResponsePaymentSource response)
        {
            return As<CardSourceResponse>(response);
        }
    }
}
