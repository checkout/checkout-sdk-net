namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The details of a certification associated with the identity verification.
    /// </summary>
    public class Certification
    {
        /// <summary>
        /// The certification type.
        /// [Optional]
        /// </summary>
        public CertificationType? Type { get; set; }

        /// <summary>
        /// The certification data. The properties returned depend on the certification type.
        /// [Optional]
        /// </summary>
        public DiatfCertificationData Data { get; set; }
    }
}
