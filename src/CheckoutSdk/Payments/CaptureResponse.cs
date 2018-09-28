using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// The capture response indicates the capture has been accepted for deferred processing.
    /// </summary>
    public class CaptureResponse : Resource
    {
        /// <summary>
        /// Gets the unique identifier for the capture action.
        /// </summary>
        public string ActionId { get; set; }
        
        /// <summary>
        /// Gets your reference for the capture request.
        /// </summary>
        public string Reference { get; set; }
    }
}