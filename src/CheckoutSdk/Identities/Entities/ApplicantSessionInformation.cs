using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Identities.Entities
{
    public class ApplicantSessionInformation
    {
        /// <summary>
        /// The applicant's IP address during the attempt
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The documents the applicant selected in order.
        /// </summary>
        public List<SelectedDocument> SelectedDocuments { get; set; }
    }
}