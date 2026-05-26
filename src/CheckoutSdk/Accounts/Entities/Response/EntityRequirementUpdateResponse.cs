using System;
using System.Runtime.Serialization;
using Checkout.Common;

namespace Checkout.Accounts.Entities.Response
{
    public enum EntityRequirementUpdateStatus
    {
        [EnumMember(Value = "processing")]
        Processing
    }

    /// <summary>
    /// Acknowledges that a requirement response has been accepted for processing.
    /// </summary>
    public class EntityRequirementUpdateResponse : Resource
    {
        /// <summary>
        /// The unique identifier of the requirement.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Processing status of the submitted response. While the response is processing, the requirement
        /// is no longer retrievable via the GET endpoints; if validation fails downstream the requirement
        /// may reappear.
        /// [Optional]
        /// Enum: "processing"
        /// </summary>
        public EntityRequirementUpdateStatus? Status { get; set; }

        /// <summary>
        /// The date and time, in ISO 8601 UTC format, the response was accepted.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? SubmittedAt { get; set; }
    }
}
