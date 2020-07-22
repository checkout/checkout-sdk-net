using System;

namespace Checkout.Instruments
{
    /// <summary>
    /// Defines a request for a payment instrument.
    /// </summary>
    public class InstrumentRequest
    {
        /// <summary>
        /// Creates a new <see cref="InstrumentRequest"/> instance.
        /// </summary>
        /// <param name="type">The payment instrument type.</param>
        /// <param name="token">The payment instrument type.</param>
        public InstrumentRequest(string type, string token)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type), "The payment instrument type is required.");
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token), "The payment instrument token is required.");

            Type = type;
            Token = token;
        }

        /// <summary>
        /// Gets the type of the payment instrument.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the token of the payment instrument.
        /// </summary>
        public string Token { get; }
    }
}
