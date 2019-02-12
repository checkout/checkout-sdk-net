using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Represents an Alternative Payment source for a payment request.
    /// </summary>
    public class AlternativePaymentSource : Dictionary<string, object>, IRequestSource
    {
        private const string TypeField = "type";

        /// <summary>
        /// Creates a new <see cref="AlternativePaymentSource"/> instance.
        /// </summary>
        /// <param name="type">The type of the Alternative Payment source.</param>
        public AlternativePaymentSource(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("The alternative payment source type is required.", nameof(type));

            Type = type;
        }

        /// <summary>
        /// Gets or sets the type of source.
        /// </summary>
        public string Type
        {
            get { return this[TypeField].ToString(); }
            set { this[TypeField] = value; }
        }
    }
}
