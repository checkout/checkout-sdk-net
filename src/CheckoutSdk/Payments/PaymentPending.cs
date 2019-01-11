using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Indicates the payment is pending, either for deferred processing or awaiting redirect.
    /// </summary>
    public class PaymentPending : Resource
    {
        /// <summary>
        /// Gets the unique identifier of the payment.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets the status of the payment.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Gets your reference for the payment request.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets the customer to which this payment is linked.
        /// </summary>
        public CustomerResponse Customer { get; set; }
        
        /// <summary>
        /// Gets the 3D-Secure enrollment status.
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDSEnrollment ThreeDS { get; set; }

        /// <summary>
        /// Determines whether the payment requires a redirect.
        /// </summary>
        /// <returns>True if a redirect is required, otherwise False.</returns>
        public bool RequiresRedirect() => HasLink("redirect");
        
        /// <summary>
        /// Gets the redirect link.
        /// </summary>
        /// <returns>The link if present, otherwise null.</returns>
        public Link GetRedirectLink() => GetLink("redirect");
    }
}