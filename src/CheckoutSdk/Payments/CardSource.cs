using System;
using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// Represents a full card source for a payment request (PCI compliant merchants only).
    /// </summary>
    public class CardSource : RequestSource
    {
        public const string TypeName = "card";

        /// <summary>
        /// Creates a new <see cref="CardSource"/> instance with the specified card details.
        /// </summary>
        /// <param name="number">The card number.</param>
        /// <param name="expiryMonth">The two-digit expiry month of the card.</param>
        /// <param name="expiryYear">The four-digit expiry year of the card.</param>
        public CardSource(string number, int expiryMonth, int expiryYear) : base(number, expiryMonth, expiryYear, TypeName) {}
        
    }
}