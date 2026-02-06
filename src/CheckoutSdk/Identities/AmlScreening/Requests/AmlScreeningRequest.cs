using Checkout.Identities.Common;

namespace Checkout.Identities.AmlScreening.Requests
{
    public class AmlScreeningRequest
    {
        /// <summary>
        /// Applicant ID to screen (Required)
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Reference for the AML screening (Required, max 255 characters)
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Search parameters for AML screening (Optional)
        /// </summary>
        public SearchParameters SearchParameters { get; set; }
    }
}