using Checkout.Common;

namespace Checkout.Payments
{
    public class VoidResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the void action
        /// </summary>
        public string ActionId { get; set; }
        /// <summary>
        /// Your reference for the void request
        /// </summary>
        public string Reference { get; set; }
    }
}