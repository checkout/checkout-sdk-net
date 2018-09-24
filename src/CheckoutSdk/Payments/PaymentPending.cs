using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class PaymentPending : Resource
    {
        /// <summary>
        /// Payment unique identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The status of the payment
        /// </summary>
        public PaymentStatus? Status { get; set; }
        /// <summary>
        /// Your reference for the payment request
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// The customer to which this payment is linked
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// Provides 3D-Secure enrollment status
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsEnrollment ThreeDs { get; set; }
        public bool RequiresRedirect() => HasLink("redirect");
        public Link GetRedirectLink() => GetLink("redirect");
    }
}