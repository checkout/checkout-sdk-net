namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Requests
{
    /// <summary>
    /// Request to enroll an entity with Google Pay.
    /// Required (API): entity_id, email_address, accept_terms_of_service.
    /// </summary>
    public class GooglePayEnrollmentRequest
    {
        /// <summary>
        /// The unique identifier of the entity to enroll.
        /// [Required]
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The email address of the user accepting the Google terms of service.
        /// [Required]
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Indicates acceptance of the Google terms of service. Must be true to proceed with enrollment.
        /// [Required]
        /// </summary>
        public bool AcceptTermsOfService { get; set; }
    }
}
