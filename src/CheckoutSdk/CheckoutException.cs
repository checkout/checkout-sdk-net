using System;

namespace Checkout
{
    public class CheckoutException : Exception
    {
        public CheckoutException(string message) : base(message)
        {
            
        }
    }
}