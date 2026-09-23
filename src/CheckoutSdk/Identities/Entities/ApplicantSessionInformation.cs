using System.Collections.Generic;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The details of the attempt.
    /// </summary>
    public class ApplicantSessionInformation
    {
        /// <summary>
        /// The applicant's IP address during the attempt.
        /// [Optional]
        /// Example: 123.4.5.6
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The number of sessions the applicant opened during the attempt.
        /// [Optional]
        /// Example: 3
        /// </summary>
        public int? NumberOfSessions { get; set; }

        /// <summary>
        /// The user agent of the browser the applicant used during the attempt.
        /// [Optional]
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The type of device the applicant used to start the attempt.
        /// [Optional]
        /// </summary>
        public InitialDevice? InitialDevice { get; set; }

        /// <summary>
        /// The documents the applicant selected in order.
        /// Returned for identity verification attempts only; not returned for face
        /// authentication attempts.
        /// [Optional]
        /// </summary>
        public List<SelectedDocument> SelectedDocuments { get; set; }
    }
}
