using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class Action : Resource
    {
        /// <summary>
        /// The unique identifier of the payment action
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The type of action
        /// </summary>
        public ActionType? Type { get; set; }
        /// <summary>
        /// The date/time the action was processed
        /// </summary>
        public DateTime ProcessedOn { get; set; }
        /// <summary>
        /// The action amount
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// The acquirer authorization code for cards
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// Gateway response code
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// The Gateway response summary
        /// </summary>
        public string ResponseSummary { get; set; }
        /// <summary>
        /// Your reference for the action
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// Set of key/value pairs that you can attach to an action
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}