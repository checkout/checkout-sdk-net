namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.PTwoFourSource
{
    /// <summary>
    /// p24 source Class
    /// The source of the payment
    /// </summary>
    public class PTwoFourSource : AbstractSource
    {

        /// <summary>
        /// P24-generated payment descriptor, which contains the requested billing descriptor or the merchant's default
        /// descriptor (subject to truncation).
        /// [Optional]
        /// </summary>
        public string PTwoFourDescriptor { get; set; }

    }
}
