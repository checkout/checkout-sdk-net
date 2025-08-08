namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.PaymentGetResponseKlarnaSourceSource.AccountHolder.BillingAddress
{
    /// <summary>
    /// billing_address
    /// Address of the account holder.
    /// </summary>
    public class BillingAddress
    {

        /// <summary>
        /// Postal code of the account holder.
        /// [Required]
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// City of the account holder.
        /// [Required]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// ISO 3166 alpha-2 account holder country code.
        /// [Required]
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Street address of the account holder.
        /// [Optional]
        /// </summary>
        public string AddressLineOne { get; set; }

        /// <summary>
        /// Street address of the account holder.
        /// [Optional]
        /// </summary>
        public string AddressLineTwo { get; set; }

    }
}
