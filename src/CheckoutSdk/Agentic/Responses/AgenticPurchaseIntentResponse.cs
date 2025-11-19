using Checkout.Agentic.Requests;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Response from purchase intent creation
    /// </summary>
    public class AgenticPurchaseIntentResponse : Resource
    {
        /// <summary>
        /// The purchase intent ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The scheme of the purchase intent
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The status of the purchase intent
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The tokenid of the purchase intent
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// The device information of the purchase intent
        /// </summary>
        public DeviceInfo DeviceData { get; set; }

        /// <summary>
        /// The customer prompt of the purchase intent
        /// </summary>
        public string CustomerPrompt { get; set; }

        /// <summary>
        /// List of mandates for the purchase intent
        /// </summary>
        public Mandate[] Mandates { get; set; }
    }
}