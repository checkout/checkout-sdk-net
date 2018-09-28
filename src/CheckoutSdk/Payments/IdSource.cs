using System;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a payment source that references an existing payment source ID.
    /// </summary>
    public class IdSource : IRequestSource
    {
        public const string TypeName = "id";

        /// <summary>
        /// Creates a new instance of <see cref="IdSource"/>.
        /// </summary>
        /// <param name="id">The payment source identifier for example, a card source identifier.</param>
        public IdSource(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("The source ID is required", nameof(id));

            Id = id;
        }

        /// <summary>
        /// Gets the payment source identifer for example, a card source identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets or sets card verification value/code for card sources.
        /// </summary>
        public string Cvv { get; set; }
        
        /// <summary>
        /// Gets the type of source.
        /// </summary>
        public string Type => TypeName;
    }
}
