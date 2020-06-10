using System;
using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// Represents a full card source for a payment request (PCI compliant merchants only).
    /// </summary>
    public class DlocalCardSource : RequestSource
    {
        public const string TypeName = "dlocal";

        /// <summary>
        /// Creates a new <see cref="DlocalCardSource"/> instance with the specified card details.
        /// </summary>
        /// <param name="number">The card number.</param>
        /// <param name="name">The name on card.</param>
        /// <param name="expiryMonth">The two-digit expiry month of the card.</param>
        /// <param name="expiryYear">The four-digit expiry year of the card.</param>
        public DlocalCardSource(string number, int expiryMonth, int expiryYear, string name) : base(number, expiryMonth, expiryYear, TypeName)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name on card is required for dLocal payments", nameof(name));
            Name = name;
        }
    }
}