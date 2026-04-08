using System.Collections.Generic;

namespace Checkout.ComplianceRequests.Requests
{
    /// <summary>
    /// Groups the responded fields by party (sender/recipient).
    /// </summary>
    public class ComplianceRespondedFields
    {
        /// <summary>
        /// The fields provided for the sender.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public IList<ComplianceRespondedField> Sender { get; set; }

        /// <summary>
        /// The fields provided for the recipient.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public IList<ComplianceRespondedField> Recipient { get; set; }
    }
}
