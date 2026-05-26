namespace Checkout.Accounts.Entities.Requirements
{
    /// <summary>
    /// Request body used to resolve a requirement. The shape of value is defined by the requirement's
    /// _schema (returned from GET /accounts/entities/{id}/requirements/{requirementId}).
    /// </summary>
    public class EntityRequirementUpdateRequest
    {
        /// <summary>
        /// The response to the requirement. The expected shape depends on the requirement and is defined
        /// by the JSON Schema returned in the requirement details response. Common shapes include a file
        /// reference (for document uploads), a primitive value, or a structured object.
        /// [Required]
        /// </summary>
        public object Value { get; set; }
    }
}
