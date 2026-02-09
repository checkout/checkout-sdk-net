using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Checkout.Common;
using Checkout.Identities.IdDocumentVerification.Requests;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    public class IdDocumentVerificationReportResponse : Resource
    {
        /// <summary>
        /// The pre-signed URL to the PDF report
        /// [Required]
        /// </summary>
        public string SignedUrl { get; set; }
    }
}