using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Accounts.Entities.Requirements
{
    /// <summary>
    /// A pending requirement that the sub-entity must resolve to remain compliant or unlock capabilities.
    /// </summary>
    public class EntityRequirementListItem : Resource
    {
        /// <summary>
        /// The unique identifier of the requirement.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The ID of the resource (sub-entity or representative) the requirement applies to.
        /// [Optional]
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// The type of resource the requirement applies to derived from the resource's URN.
        /// Common values include company, individual, and representative. Defaults to entity if the URN
        /// cannot be parsed.
        /// [Optional]
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// The reason the requirement was raised.
        /// [Optional]
        /// Enum: "periodic_review" "attestation"
        /// </summary>
        public EntityRequirementReason? Reason { get; set; }

        /// <summary>
        /// Priority level of this requirement. "high" by default, "critical" if deadline is within 7 days.
        /// [Optional]
        /// Enum: "high" "critical"
        /// </summary>
        public EntityRequirementPriority? Priority { get; set; }

        /// <summary>
        /// The date and time, in ISO 8601 UTC format, by which the requirement must be resolved.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// The schema-registry URN identifying the field this requirement applies to.
        /// Format: urn:object:{resource_type}#{resource_id}#field:{field_name}.
        /// [Optional]
        /// </summary>
        public string Urn { get; set; }

        /// <summary>
        /// Dot-notation path of the field on the resource that needs to be supplied or updated.
        /// May be null for ad-hoc requirements (such as additional documents or free-text responses)
        /// that do not map to a specific field on the entity.
        /// [Optional]
        /// </summary>
        public string FieldPath { get; set; }

        /// <summary>
        /// The schema-registry URN of the field, from the public mapping definition.
        /// May be null if no mapping exists.
        /// [Optional]
        /// </summary>
        public string FieldUrn { get; set; }

        /// <summary>
        /// Optional metadata from the property mapping definition. Shape varies by requirement.
        /// [Optional]
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }
    }
}
