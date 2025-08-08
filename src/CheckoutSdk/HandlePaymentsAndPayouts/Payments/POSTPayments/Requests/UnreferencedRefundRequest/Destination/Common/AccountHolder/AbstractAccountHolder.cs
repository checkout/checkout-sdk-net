namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder
{
    /// <summary>
    /// Abstract account_holder Class
    /// The unreferenced refund destination account holder.
    /// </summary>
    public abstract class AbstractAccountHolder
    {
        public AccountHolderType? Type;

        protected AbstractAccountHolder(AccountHolderType type)
        {
            Type = type;
        }
    }
}