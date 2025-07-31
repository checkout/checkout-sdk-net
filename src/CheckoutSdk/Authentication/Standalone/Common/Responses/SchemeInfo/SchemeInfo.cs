namespace Checkout.Authentication.Standalone.Common.Responses.SchemeInfo
{
    /// <summary>
    /// scheme_info
    /// Indicates scheme-specific information
    /// </summary>
    public class SchemeInfo
    {
        /// <summary>
        /// Indicates which scheme was used to perform authentication
        /// [Optional]
        /// </summary>
        public NameType? Name { get; set; }

        /// <summary>
        /// Risk score calculated by Directory Server (DS). Cartes Bancaires 3D Secure 2 only
        /// [Optional]
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// Identification of the algorithm used by the ACS to calculate the Authentication Value indicated
        /// [Optional]
        /// </summary>
        public string Avalgo { get; set; }
    }
}