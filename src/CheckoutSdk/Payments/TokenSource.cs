using System;
using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a payment source for wallet tokens (Apple Pay/Google Pay) or card tokens generated by Checkout JS/Frames.
    /// </summary>
    public class TokenSource : IRequestSource
    {
        public const string TypeName = "token";

        /// <summary>
        /// Creates a new <see cref="TokenSource"/> instance.
        /// </summary>
        /// <param name="token">The Checkout token for example a card, wallet or alternative payment token.</param>
        public TokenSource(string token)
        {
            if(string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("The token must be provided.", nameof(token));
                
            Token = token;
        }

        /// <summary>
        /// Gets the Checkout token for example a card, wallet or alternative payment token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets or sets payment source owner's billing address. This will override the billing address specified during tokenization.
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets the type of source.
        /// </summary>
        public string Type => TypeName;
    }
}