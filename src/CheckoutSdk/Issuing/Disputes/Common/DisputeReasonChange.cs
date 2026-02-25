namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// The change to the dispute reason and your justification for changing it.
    /// [Beta]
    /// </summary>
    public class DisputeReasonChange
    {
        /// <summary>
        /// The updated four-digit scheme-specific reason code for the chargeback.
        /// [Required]
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Your justification for changing the dispute reason.
        /// [Required]
        /// &lt;= 13000 characters
        /// </summary>
        public string Justification { get; set; }
    }
}