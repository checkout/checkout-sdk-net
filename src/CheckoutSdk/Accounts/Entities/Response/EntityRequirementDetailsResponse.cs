using System.Collections.Generic;
using Newtonsoft.Json;

using Checkout.Accounts.Entities.Common.Requirements;

namespace Checkout.Accounts.Entities.Response
{
    /// <summary>
    /// Detailed information about a single requirement.
    /// </summary>
    public class EntityRequirementDetailsResponse : EntityRequirementListItem
    {
        /// <summary>
        /// A user-facing explanation of what is needed to resolve the requirement.
        /// [Optional]
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// JSON Schema that the value supplied to PUT /accounts/entities/{id}/requirements/{requirementId}
        /// must conform to. The shape varies by requirement type (for example: an identity document upload,
        /// a free-text response, or a structured object). Validate the value client-side against this schema
        /// before submitting. May be null if no schema is registered for the requirement's field.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "_schema")]
        public IDictionary<string, object> Schema { get; set; }
    }
}
