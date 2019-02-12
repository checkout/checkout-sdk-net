using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// The alternative payment used to complete a payment request. 
    /// </summary>
    public class AlternativePaymentSourceResponse : Dictionary<string, object>, IResponseSource
    {
        /// <summary>
        /// Gets or sets the type of source.
        /// </summary>
        public string Type { get; set; }
    }
}