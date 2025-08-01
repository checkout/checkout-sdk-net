namespace Checkout.Authentication.Standalone.Common.Responses.Exemption
{
    /// <summary>
    /// exemption
    /// Details related to exemption present in 3DS flow
    /// </summary>
    public class Exemption
    {
        /// <summary>
        /// Indicates merchant requested exemption
        /// [Optional]
        /// </summary>
        public string Requested { get; set; }

        /// <summary>
        /// Indicates Issuer accepted or applied exemption
        /// [Optional]
        /// </summary>
        public AppliedType? Applied { get; set; }

        /// <summary>
        /// Indicates Cartes Bancaire specific exemption value
        /// [Optional]
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Details of the trusted listing status of the merchant.
        /// [Optional]
        /// </summary>
        public TrustedBeneficiary.TrustedBeneficiary TrustedBeneficiary { get; set; }
    }
}