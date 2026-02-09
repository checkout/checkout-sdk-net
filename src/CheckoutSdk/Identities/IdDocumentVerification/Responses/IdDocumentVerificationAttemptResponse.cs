using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    public class IdDocumentVerificationAttemptResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the ID document verification attempt
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the ID document verification attempt
        /// [Required]
        /// </summary>
        public IdDocumentVerificationAttemptStatus Status { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// [Required]
        /// </summary>
        public List<ResponseCode> ResponseCodes { get; set; }

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
    }
}