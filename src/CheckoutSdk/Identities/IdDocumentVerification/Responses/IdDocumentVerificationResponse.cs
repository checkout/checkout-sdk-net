using System;
using System.Collections.Generic;

using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    public class IdDocumentVerificationResponse : Resource
    {
        /// <summary>
        /// Your configuration ID
        /// [Required]
        /// </summary>
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The ID document verification's unique identifier
        /// [Required]
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// [Required]
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// [Required]
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The ID document verification status
        /// [Required]
        /// </summary>
        public IdDocumentVerificationStatus Status { get; set; }

        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// [Required]
        /// </summary>
        public List<ResponseCode> ResponseCodes { get; set; }

        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The applicant's identity document details
        /// </summary>
        public DocumentDetails Document { get; set; }
    }
}