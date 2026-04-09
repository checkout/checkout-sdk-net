namespace Checkout.ComplianceRequests.Requests
{
    /// <summary>
    /// Describes a single response field provided for a compliance request.
    /// </summary>
    public class ComplianceRespondedField
    {
        /// <summary>
        /// The field name being responded to.
        /// [Required]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value provided for the field.
        /// [Required]
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Indicates whether the value is unavailable for this field.
        /// [Required]
        /// </summary>
        public bool NotAvailable { get; set; }
    }
}
