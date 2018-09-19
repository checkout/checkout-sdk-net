using Checkout.Common;

namespace Checkout.Payments
{
    public class CaptureResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the capture action
        /// </summary>
        public string ActionId { get; set; }
        /// <summary>
        /// Your reference for the capture request
        /// </summary>
        public string Reference { get; set; }
    }
}