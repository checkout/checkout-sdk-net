namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder's government document identification, for example a Social Security Number (SSN).
    /// </summary>
    public class AchAccountHolderIdentification
    {
        /// <summary>
        /// The document type.
        /// [Optional]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The country where the document was issued.
        /// [Optional]
        /// </summary>
        public string IssuingCountry { get; set; }

        /// <summary>
        /// The document number.
        /// [Optional]
        /// </summary>
        public string Number { get; set; }
    }
}
