using System.Collections.Generic;

namespace Checkout.ComplianceRequests.Responses
{
    /// <summary>
    /// Groups the requested fields by party (sender/recipient).
    /// </summary>
    public class ComplianceRequestedFields
    {
        /// <summary>The list of requested sender details.</summary>
        public IList<ComplianceRequestedField> Sender { get; set; }

        /// <summary>The list of requested recipient details.</summary>
        public IList<ComplianceRequestedField> Recipient { get; set; }
    }
}
