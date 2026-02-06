using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Checkout.Common;
using Checkout.Identities.IdDocumentVerification.Requests;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    public class IdDocumentVerificationResponse : Resource
    {
        /// <summary>
        /// The ID document verification's unique identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Your configuration ID
        /// </summary>
        [JsonProperty("user_journey_id")]
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The applicant's unique identifier
        /// </summary>
        [JsonProperty("applicant_id")]
        public string ApplicantId { get; set; }

        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// </summary>
        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// </summary>
        [JsonProperty("modified_on")]
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The ID document verification status
        /// </summary>
        [JsonProperty("status")]
        public IdDocumentVerificationStatus Status { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// </summary>
        [JsonProperty("response_codes")]
        public List<IdDocumentVerificationResponseCode> ResponseCodes { get; set; }

        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        [JsonProperty("declared_data")]
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The applicant's identity document details
        /// </summary>
        [JsonProperty("document")]
        public DocumentDetails Document { get; set; }
    }

    public class IdDocumentVerificationAttemptResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the ID document verification attempt
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The status of the ID document verification attempt
        /// </summary>
        [JsonProperty("status")]
        public IdDocumentVerificationAttemptStatus Status { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// </summary>
        [JsonProperty("response_codes")]
        public List<IdDocumentVerificationResponseCode> ResponseCodes { get; set; }

        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// </summary>
        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// </summary>
        [JsonProperty("modified_on")]
        public DateTime? ModifiedOn { get; set; }
    }

    public class IdDocumentVerificationAttemptsResponse : Resource
    {
        /// <summary>
        /// The total number of attempts
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// The number of attempts you want to skip
        /// </summary>
        [JsonProperty("skip")]
        public int Skip { get; set; }

        /// <summary>
        /// The maximum number of attempts you want returned
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// The details of the attempts
        /// </summary>
        [JsonProperty("data")]
        public List<IdDocumentVerificationAttemptResponse> Data { get; set; }
    }

    public class IdDocumentVerificationReportResponse : Resource
    {
        /// <summary>
        /// The pre-signed URL to the PDF report
        /// </summary>
        [JsonProperty("signed_url")]
        public string SignedUrl { get; set; }
    }

    public class DocumentDetails
    {
        /// <summary>
        /// The type of identity document
        /// </summary>
        [JsonProperty("document_type")]
        public string DocumentType { get; set; }

        /// <summary>
        /// The document issuing country
        /// </summary>
        [JsonProperty("document_issuing_country")]
        public string DocumentIssuingCountry { get; set; }

        /// <summary>
        /// The pre-signed URL to the captured image of the front of the document
        /// </summary>
        [JsonProperty("front_image_signed_url")]
        public string FrontImageSignedUrl { get; set; }

        /// <summary>
        /// The pre-signed URL to the captured image of the back of the document
        /// </summary>
        [JsonProperty("back_image_signed_url")]
        public string BackImageSignedUrl { get; set; }

        /// <summary>
        /// The pre-signed URL to the captured image of the signature
        /// </summary>
        [JsonProperty("signature_image_signed_url")]
        public string SignatureImageSignedUrl { get; set; }

        /// <summary>
        /// The applicant's full name
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// The applicant's birth date
        /// </summary>
        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        /// <summary>
        /// The applicant's first names
        /// </summary>
        [JsonProperty("first_names")]
        public string FirstNames { get; set; }

        /// <summary>
        /// The applicant's last name
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The applicant's last name at birth
        /// </summary>
        [JsonProperty("last_name_at_birth")]
        public string LastNameAtBirth { get; set; }

        /// <summary>
        /// The applicant's birth place
        /// </summary>
        [JsonProperty("birth_place")]
        public string BirthPlace { get; set; }

        /// <summary>
        /// The applicant's nationality
        /// </summary>
        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// The applicant's gender
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// The applicant's personal number
        /// </summary>
        [JsonProperty("personal_number")]
        public string PersonalNumber { get; set; }

        /// <summary>
        /// The tax identification number (TIN) extracted from the document
        /// </summary>
        [JsonProperty("tax_identification_number")]
        public string TaxIdentificationNumber { get; set; }

        /// <summary>
        /// The document number extracted from the document
        /// </summary>
        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// The document expiry date extracted from the document
        /// </summary>
        [JsonProperty("document_expiry_date")]
        public string DocumentExpiryDate { get; set; }

        /// <summary>
        /// The document issue date extracted from the document
        /// </summary>
        [JsonProperty("document_issue_date")]
        public string DocumentIssueDate { get; set; }

        /// <summary>
        /// The document's place of issue extracted from the document
        /// </summary>
        [JsonProperty("document_issue_place")]
        public string DocumentIssuePlace { get; set; }

        /// <summary>
        /// The machine-readable zone (MRZ) data extracted from the document
        /// </summary>
        [JsonProperty("document_mrz")]
        public string DocumentMrz { get; set; }
    }

    public class IdDocumentVerificationResponseCode
    {
        /// <summary>
        /// The response code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// The description of the response code
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public enum IdDocumentVerificationStatus
    {
        [JsonProperty("created")]
        Created,
        
        [JsonProperty("quality_checks_in_progress")]
        QualityChecksInProgress,
        
        [JsonProperty("checks_in_progress")]
        ChecksInProgress,
        
        [JsonProperty("approved")]
        Approved,
        
        [JsonProperty("declined")]
        Declined,
        
        [JsonProperty("retry_required")]
        RetryRequired,
        
        [JsonProperty("inconclusive")]
        Inconclusive
    }

    public enum IdDocumentVerificationAttemptStatus
    {
        [JsonProperty("checks_in_progress")]
        ChecksInProgress,
        
        [JsonProperty("checks_inconclusive")]
        ChecksInconclusive,
        
        [JsonProperty("completed")]
        Completed,
        
        [JsonProperty("quality_checks_aborted")]
        QualityChecksAborted,
        
        [JsonProperty("quality_checks_in_progress")]
        QualityChecksInProgress,
        
        [JsonProperty("terminated")]
        Terminated
    }
}