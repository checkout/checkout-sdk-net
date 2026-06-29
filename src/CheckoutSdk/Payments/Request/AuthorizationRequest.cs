using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class AuthorizationRequest
    {
        /// <summary>
        /// The amount to increase the authorization by. Omit or provide 0 to extend the authorization validity period.
        /// [Optional]
        /// min 0
        /// max 99999999
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// A reference you can later use to identify this authorization request.
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A set of key-value pairs to attach to the authorization request for structured additional information.
        /// The metadata object only supports primitive data types. Objects and arrays are not supported.
        /// [Optional]
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}