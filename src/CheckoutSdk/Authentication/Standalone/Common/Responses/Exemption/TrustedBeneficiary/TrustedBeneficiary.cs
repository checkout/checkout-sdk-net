namespace Checkout.Authentication.Standalone.Common.Responses.Exemption.TrustedBeneficiary
{
    /// <summary>
    /// trusted_beneficiary
    /// Details of the trusted listing status of the merchant.
    /// </summary>
    public class TrustedBeneficiary
    {
        /// <summary>
        /// Enables the communication of trusted beneficiary status between the Access Control Server (ACS), the
        /// Directory Server (DS), and the 3D Secure (3DS) Requestor.
        /// • Y = 3DS Requestor is allowlisted by cardholder • N = 3DS Requestor is not allowlisted by cardholder • E =
        /// Not eligible as determined by issuer • P = Pending confirmation by cardholder • R = Cardholder rejected • U
        /// = Allowlist status unknown, unavailable, or does not apply
        /// [Optional]
        /// </summary>
        public StatusType? Status { get; set; }

        /// <summary>
        /// The system setting trusted beneficiary status.
        /// • 01 = 3DS Server  • 02 = DS  • 03 = ACS  • 80-99 = DS-specific values
        /// [Optional]
        /// </summary>
        public string Source { get; set; }
    }
}