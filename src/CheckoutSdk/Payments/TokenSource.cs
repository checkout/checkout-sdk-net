using System;
using Checkout.Common;

namespace Checkout.Payments
{
    public class TokenSource : IPaymentSource
    {
        public const string TypeName = "token";

        /// <summary>
        /// Token source of the payment
        /// </summary>
        /// <param name="token">The Checkout token for example a card, wallet or alternative payment token</param>
        public TokenSource(string token)
        {
            if(string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token must be provided.", nameof(token));
            Token = token;
        }

        /// <summary>
        /// The Checkout token for example a card, wallet or alternative payment token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// The payment source owner's billing address. This will override the billing address specified during tokenisation
        /// </summary>
        public Address BillingAddress { get; set; }

        public string Type => TypeName;
    }
}