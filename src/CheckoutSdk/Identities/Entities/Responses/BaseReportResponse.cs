using Checkout.Common;

namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for report responses
    /// </summary>
    public class BaseReportResponse : Resource
    {
        /// <summary>
        /// The pre-signed URL to the PDF report
        /// [Required]
        /// </summary>
        public string SignedUrl { get; set; }
    }
}