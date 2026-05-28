namespace Checkout.Accounts.Entities.Common
{
    /// <summary>
    /// Captured as evidence of the end-user's consent to onboarding.
    /// </summary>
    public class Submitter
    {
        /// <summary>
        /// IP address of the end-user (the sub-entity's representative) submitting the onboarding request.
        /// [Required]
        /// </summary>
        public string IpAddress { get; set; }
    }
}
