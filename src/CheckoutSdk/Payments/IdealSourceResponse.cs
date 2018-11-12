using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// The card used to complete a payment request. 
    /// </summary>
    public class IdealSourceResponse : IResponseSource
    {        
        /// <summary>
        /// Gets or sets the type of source.
        /// </summary>
        public string Type { get; set; }
    }
}