namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    CardSource.AccountHolder
{
    /// <summary>
    /// Abstract account_holder Class
    /// Information about the account holder of the card
    /// </summary>
    public abstract class AbstractAccountHolder
    {
        public AccountHolderType Type;

        protected AbstractAccountHolder(AccountHolderType type)
        {
            Type = type;
        }
    }
}