using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    /// <summary>
    /// Represents the <see cref="DisputeEvidenceResponse"/>.
    /// </summary>
    public class DisputeEvidenceResponse : Resource
    {
        /// <summary>
        /// Gets the dispute evidence.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> DisputeEvidence { get; set; }
    }
}
