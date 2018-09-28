using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// The refund response indicates the refund has been accepted for deferred processing.
    /// </summary>
    public class RefundResponse : Resource
    {
        /// <summary>
        /// Gets the unique identifier for the refund action.
        /// </summary>
        public string ActionId { get; set; }

        
        /// <summary>
        /// Gets your reference for the refund request.
        /// </summary>       
        public string Reference { get; set; }
    }
}