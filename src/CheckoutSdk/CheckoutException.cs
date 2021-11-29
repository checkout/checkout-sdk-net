using System;

namespace Checkout
{
    public class CheckoutException : Exception
    {
        public CheckoutException(string message) : base(message)
        {
        }

        protected CheckoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}