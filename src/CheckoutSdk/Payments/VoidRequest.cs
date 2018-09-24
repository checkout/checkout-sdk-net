using System.Collections.Generic;

namespace Checkout.Payments
{
    public class VoidRequest
    {
        /// <summary>
        /// A reference you can later use to identify this void request
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// Set of key/value pairs that you can attach to the void request. It can be useful for storing additional information in a structured format
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}