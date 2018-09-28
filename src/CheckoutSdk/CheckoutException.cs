using System;

namespace Checkout
{
    /// <summary>
    /// Base class for exceptions thrown by the Checkout.com SDK for .NET.
    /// </summary>
    public class CheckoutException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutException"/> instance with the provided message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <returns></returns>
        public CheckoutException(string message) : base(message)
        {
            
        }
    }
}