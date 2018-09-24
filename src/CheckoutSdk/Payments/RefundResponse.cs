using Checkout.Common;

namespace Checkout.Payments
{
    public class RefundResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the refund action
        /// </summary>
        public string ActionId { get; set; }
        /// <summary>
        /// Your reference for the refund request
        /// </summary>
        public string Reference { get; set; }
    }
}