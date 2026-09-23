namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The certification data returned for the diatf certification type.
    /// </summary>
    public class DiatfCertificationData
    {
        /// <summary>
        /// The GPG 45 identity profile the verification meets.
        /// [Optional]
        /// </summary>
        public Gpg45Profile? Gpg45Profile { get; set; }

        /// <summary>
        /// The level of confidence in the verified identity.
        /// [Optional]
        /// </summary>
        public LevelOfConfidence? LevelOfConfidence { get; set; }

        /// <summary>
        /// The outcome of the applicant's right to work check.
        /// [Optional]
        /// Example: GRANTED
        /// </summary>
        public string RightToWork { get; set; }
    }
}
