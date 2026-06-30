namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder details returned by Klarna after the shopper completes verification.
    /// </summary>
    public class KlarnaAccountHolder
    {
        /// <summary>
        /// The full name of the account holder.
        /// [Optional] readOnly
        /// </summary>
        public string Name { get; set; }
    }
}
