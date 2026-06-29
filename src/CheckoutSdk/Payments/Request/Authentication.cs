using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class Authentication
    {
        /// <summary>
        /// An ordered list of preferred authentication experiences to attempt for the payment request.
        /// [Optional]
        /// Enum: "google_spa" "3ds"
        /// </summary>
        public IList<PreferredExperiences> PreferredExperiences { get; set; }
    }
}