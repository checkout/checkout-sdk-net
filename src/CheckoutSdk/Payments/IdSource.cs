using System;

namespace Checkout.Payments
{
    public class IdSource : IPaymentSource
    {
        public const string TypeName = "id";

        /// <summary>
        /// ID source payment type
        /// </summary>
        /// <param name="id">The payment source identifier for example, a card source identifier</param>
        /// <param name="cvv">The card verification value/code (for card sources). 3 digits, except for Amex (4 digits).</param>
        public IdSource(string id, string cvv = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"Source ID required", nameof(id));

            Id = id;
            Cvv = cvv;
        }

        /// <summary>
        /// The payment source identifer for example, a card source identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The card verification value/code (for card sources). 3 digits, except for Amex (4 digits).
        /// </summary>
        public string Cvv { get; }
        
        public string Type => TypeName;
    }
}
