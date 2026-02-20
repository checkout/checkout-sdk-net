namespace Checkout.Identities.Entities
{
    public class ClientInformation
    {
        /// <summary>
        /// The applicant's residence country (ISO alpha-2 country code)
        /// </summary>
        public string PreSelectedResidenceCountry { get; set; }

        /// <summary>
        /// The language for the user interface (IETF BCP 47 language tag)
        /// </summary>
        public string PreSelectedLanguage { get; set; }
    }
}


