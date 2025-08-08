namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source
{
    /// <summary>
    /// Abstract source Class
    /// The source of the payment
    /// </summary>
    public abstract class AbstractSource
    {

        public AbstractSourceType? Type;

        protected AbstractSource(AbstractSourceType type)
        {
            Type = type;
        }

    }
}
