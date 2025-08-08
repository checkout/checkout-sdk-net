namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source
{
    /// <summary>
    /// Abstract source Class
    /// The source of the payment
    /// </summary>
    public abstract class AbstractSource
    {
        public SourceType? Type;

        protected AbstractSource(SourceType type)
        {
            Type = type;
        }
    }
}