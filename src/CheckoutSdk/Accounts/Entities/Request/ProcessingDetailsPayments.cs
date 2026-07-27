namespace Checkout.Accounts.Entities.Request
{
    public class ProcessingDetailsPayments
    {
        /// <summary>
        /// ACH-specific processing details.
        /// </summary>
        public ProcessingDetailsAch Ach { get; set; }
    }
}
