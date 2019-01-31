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
        public AlternativePaymentSource(string type) {
            Type = type;
        }

        /// <summary>
        /// Gets or sets the type of source.
        /// </summary>
        public string Type {
            get { return this[TypeField].ToString(); }
            set { this[TypeField] = value; }
        }
    }
}
