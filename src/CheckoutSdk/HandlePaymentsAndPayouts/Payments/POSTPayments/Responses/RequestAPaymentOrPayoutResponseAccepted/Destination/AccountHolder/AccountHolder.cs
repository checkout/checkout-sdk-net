namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseAccepted.
    Destination.AccountHolder
{
    public class AccountHolder
    {
        /// <summary>
        /// The payment destination identifier (e.g., a card source identifier)
        /// = 30 characters ^(src)_(\w{26})$
        /// </summary>
        public string Id { get; set; }
    }
}