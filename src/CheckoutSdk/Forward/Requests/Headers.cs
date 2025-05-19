using System.Collections.Generic;

namespace Checkout.Forward.Requests
{
    public class Headers
    {
        /// <summary>
        ///     The raw headers to include in the forward request (Required, max 16 characters) </summary>
        public IDictionary<string, string> Raw { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     The encrypted headers to include in the forward request, as a JSON object with string values encrypted with JSON
        ///     Web Encryption (JWE) (Optional, max 8192 characters) </summary>
        public string Encrypted { get; set; }
    }
}