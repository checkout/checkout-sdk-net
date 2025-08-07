using System.Collections.Generic;

namespace Checkout.Authentication.Standalone.Common.Responses.PreferredExperiences.GoogleSpa
{
    /// <summary>
    /// google_spa
    /// Google SPA experience.
    /// </summary>
    public class GoogleSpa
    {
        /// <summary>
        /// [Optional]
        /// </summary>
        public StatusType? Status { get; set; }

        /// <summary>
        /// Reason(s) why processing the experience was unsuccessful.
        /// [Optional]
        /// </summary>
        public IList<object> Reason { get; set; }
    }
}