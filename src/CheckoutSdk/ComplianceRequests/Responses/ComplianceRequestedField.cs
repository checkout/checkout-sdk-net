namespace Checkout.ComplianceRequests.Responses
{
    /// <summary>
    /// Describes a single requested field within a compliance request.
    /// </summary>
    public class ComplianceRequestedField
    {
        /// <summary>The requested field name.</summary>
        public string Name { get; set; }

        /// <summary>The requested field type (for example, "string" or "date").</summary>
        public string Type { get; set; }

        /// <summary>The current value for the requested field, if available.</summary>
        public string Value { get; set; }
    }
}
