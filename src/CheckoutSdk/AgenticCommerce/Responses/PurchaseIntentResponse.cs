using System.Collections.Generic;
using Checkout.AgenticCommerce.Common;
using Checkout.Common;

namespace Checkout.AgenticCommerce.Responses
{
    /// <summary>
    /// Response from purchase intent creation
    /// </summary>
    public class PurchaseIntentResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the purchase intent
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The current status of the purchase intent
        /// </summary>
        public PurchaseIntentStatusType? Status { get; set; }

        /// <summary>
        /// The scheme of the purchase intent
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The tokenid of the purchase intent
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// The device information of the purchase intent
        /// </summary>
        public Device DeviceData { get; set; }

        /// <summary>
        /// The customer prompt of the purchase intent
        /// </summary>
        public string CustomerPrompt { get; set; }

        /// <summary>
        /// List of mandates for the purchase intent
        /// </summary>
        public IList<MandateExtended> Mandates { get; set; }
    }
}