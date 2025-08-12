namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Source
{
    /// <summary>
    /// source
    /// The source of the unreferenced refund.
    /// </summary>
    public class Source
    {
        /// <summary>
        /// The unreferenced refund source type.
        /// [Required]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The ID of the currency account that will fund the unreferenced refund.
        /// [Required]
        /// ^(ca)_(\w{26})$
        /// 29 characters
        /// </summary>
        public string Id { get; set; }
    }
}