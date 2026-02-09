using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Checkout.Common;
using Checkout.Identities.IdDocumentVerification.Requests;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    public class IdDocumentVerificationAttemptsResponse : Resource
    {
        /// <summary>
        /// The total number of attempts
        /// [Required]
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The number of attempts you want to skip
        /// [Required]
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The maximum number of attempts you want returned
        /// [Required]
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The details of the attempts
        /// [Required]
        /// </summary>
        public List<IdDocumentVerificationAttemptResponse> Data { get; set; }
    }
}