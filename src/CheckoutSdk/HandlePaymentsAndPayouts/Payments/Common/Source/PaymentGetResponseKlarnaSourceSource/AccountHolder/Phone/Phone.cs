namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.PaymentGetResponseKlarnaSourceSource.AccountHolder.Phone
{
    /// <summary>
    /// phone
    /// Phone number of the account holder.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// The international country calling code.
        /// [Optional]
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string Number { get; set; }
    }
}