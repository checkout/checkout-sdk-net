using System;
using System.Collections.Generic;
using Checkout.Common;

using Checkout.Identities.Entities;

namespace Checkout.Identities.FaceAuthentication.Responses
{
    public class FaceAuthenticationAttemptResponse : Resource
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt
        /// [Required]
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The unique identifier for the face authentication attempt
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The attempt status
        /// [Required]
        /// </summary>
        public FaceAuthenticationAttemptStatus Status { get; set; }

        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// [Required]
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// [Required]
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// [Required]
        /// </summary>
        public List<ResponseCode> ResponseCodes { get; set; }

        /// <summary>
        /// The applicant's details
        /// </summary>
        public ClientInformation ClientInformation { get; set; }

        /// <summary>
        /// The details of the attempt
        /// </summary>
        public ApplicantSessionInformation ApplicantSessionInformation { get; set; }
    }   
}