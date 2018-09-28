using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// The void response indicates the void has been accepted for deferred processing.
    /// </summary>
    public class VoidResponse : Resource
    {
        /// <summary>
        /// Gets the unique identifier of the void action.
        /// </summary>
        public string ActionId { get; set; }
        
        /// <summary>
        /// Gets your reference for the void request.
        /// </summary>
        public string Reference { get; set; }
    }
}