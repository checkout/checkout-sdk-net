using Checkout.Common;

namespace Checkout.Identities.Entities
{
    public class Document
    {
        public DocumentType DocumentType { get; set; }
        public CountryCode DocumentIssuingCountry { get; set; }
        public string FrontImageSignedUrl { get; set; }
        public string BackImageSignedUrl { get; set; }
        public string SignatureImageSignedUrl { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string LastNameAtBirth { get; set; }
        public string BirthPlace { get; set; }
        public CountryCode? Nationality { get; set; }
        public Gender? Gender { get; set; }
        public string PersonalNumber { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentExpiryDate { get; set; }
        public string DocumentIssueDate { get; set; }
        public string DocumentIssuePlace { get; set; }
        public string DocumentMrz { get; set; }
    }
}