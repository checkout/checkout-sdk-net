namespace Checkout.Identities.Entities
{
    public class DocumentDetails
    {
        /// <summary>
        /// The type of identity document
        /// [Required]
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// The document issuing country
        /// [Required]
        /// </summary>
        public string DocumentIssuingCountry { get; set; }

        /// <summary>
        /// The pre-signed URL to the captured image of the front of the document
        /// [Required]
        /// </summary>
        public string FrontImageSignedUrl { get; set; }        

        /// <summary>
        /// The applicant's full name
        /// [Required]
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The applicant's birth date (format YYYY-MM-DD)
        /// [Required]
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// The applicant's first names
        /// </summary>
        public string FirstNames { get; set; }

        /// <summary>
        /// The applicant's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The applicant's last name at birth
        /// </summary>
        public string LastNameAtBirth { get; set; }

        /// <summary>
        /// The applicant's birth place
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// The applicant's nationality (ISO alpha-2 country code)
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// The applicant's gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// The applicant's personal number
        /// </summary>
        public string PersonalNumber { get; set; }

        /// <summary>
        /// The tax identification number (TIN) extracted from the document
        /// </summary>
        public string TaxIdentificationNumber { get; set; }

        /// <summary>
        /// The document number extracted from the document
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// The document expiry date extracted from the document
        /// </summary>
        public string DocumentExpiryDate { get; set; }

        /// <summary>
        /// The document issue date extracted from the document
        /// </summary>
        public string DocumentIssueDate { get; set; }

        /// <summary>
        /// The document's place of issue extracted from the document
        /// </summary>
        public string DocumentIssuePlace { get; set; }

        /// <summary>
        /// The machine-readable zone (MRZ) data extracted from the document
        /// </summary>
        public string DocumentMrz { get; set; }

        /// <summary>
        /// The pre-signed URL to the captured image of the back of the document
        /// </summary>
        public string BackImageSignedUrl { get; set; }

        /// <summary>
        /// The pre-signed URL to the captured image of the signature
        /// </summary>
        public string SignatureImageSignedUrl { get; set; }
    }
}