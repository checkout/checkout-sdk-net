using System;

namespace Checkout.Payments
{
    /// <summary>
    /// Extensions for <see cref="IResponseSource"/>.
    /// </summary>
    public static class ResponseSourceExtensions
    {
        /// <summary>
        /// Casts the provided <paramref="source"/> as a card source.
        /// </summary>
        /// <param name="source">The source to cast.</param>
        /// <returns>The card source if available, otherwise null.</returns>
        public static CardSourceResponse AsCard(this IResponseSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source as CardSourceResponse;
        }

        /// <summary>
        /// Casts the provided <paramref="source"/> as an alternative payment source.
        /// </summary>
        /// <param name="source">The source to cast.</param>
        /// <returns>The alternative payment source if available, otherwise null.</returns>
        public static AlternativePaymentSourceResponse AsAlternativePayment(this IResponseSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source as AlternativePaymentSourceResponse;
        }
    }
}
