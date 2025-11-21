using System.Collections.Generic;
using Checkout.AgenticCommerce.Common;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Create an agentic commerce purchase intent
    /// </summary>
    public class PurchaseIntentUpdateRequest
    {
        /// <summary>
        /// A list of mandates associated with the purchase intent
        /// </summary>
        public IList<Mandate> Mandates { get; set; }
        
        /// <summary>
        /// A prompt or message for the customer. You can display this during the purchase process to provide additional
        /// context or instructions
        /// &lt;= 4098 characters
        /// </summary>
        public string CustomerPrompt { get; set; }
    }
}
