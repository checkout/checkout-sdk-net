namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.CardSource.AccountHolder
{
    /// <summary>
    /// Abstract account_holder Class
    /// Information about the account holder of the card
    /// </summary>
    public abstract class AbstractAccountHolder
    {

        public AbstractAccountHolderType? Type;

        protected AbstractAccountHolder(AbstractAccountHolderType type)
        {
            Type = type;
        }

    }
}
