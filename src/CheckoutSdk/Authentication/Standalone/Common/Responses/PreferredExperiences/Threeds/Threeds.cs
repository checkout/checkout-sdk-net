using System.Collections.Generic;

namespace Checkout.Authentication.Standalone.Common.Responses.PreferredExperiences.Threeds
{
    /// <summary>
    /// 3ds
    /// 3DS experience.
    /// </summary>
    public class Threeds
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