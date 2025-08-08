namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.
    Threeds
{
    /// <summary>
    /// 3ds
    /// Provides 3D Secure enrollment status if the payment was downgraded to non-3D Secure
    /// </summary>
    public class Threeds
    {
        /// <summary>
        /// Indicates whether this was a 3D Secure payment downgraded to non-3D-Secure (when attempt_n3d is specified)
        /// [Optional]
        /// </summary>
        public bool? Downgraded { get; set; }

        /// <summary>
        /// Indicates the 3D Secure enrollment status of the issuer
        /// Y - Issuer enrolled
        /// N - Customer not enrolled
        /// U - Unknown
        /// [Optional]
        /// </summary>
        public string Enrolled { get; set; }

        /// <summary>
        /// Indicates the reason why the payment was upgraded to 3D Secure.
        /// [Optional]
        /// </summary>
        public string UpgradeReason { get; set; }
    }
}