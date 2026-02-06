using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    ///     Response for retrieving the PDF report of an identity verification
    /// </summary>
    public class IdentityVerificationReportResponse : Resource
    {
        /// <summary>
        ///     The unique identifier for the identity verification
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     The PDF report content as base64 string
        /// </summary>
        [JsonProperty(PropertyName = "pdf_report")]
        public string PdfReport { get; set; }

        /// <summary>
        ///     The content type of the report
        /// </summary>
        [JsonProperty(PropertyName = "content_type")]
        public string ContentType { get; set; }

        /// <summary>
        ///     The size of the report in bytes
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long? Size { get; set; }

        /// <summary>
        ///     The filename for the report
        /// </summary>
        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        /// <summary>
        ///     Additional metadata for the report
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public Dictionary<string, object> Metadata { get; set; }
    }
}