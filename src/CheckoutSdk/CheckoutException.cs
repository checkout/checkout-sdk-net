using System;

namespace Checkout.Sdk
{
    public class CheckoutException : Exception
    {
        public CheckoutException(string message) : base(message)
        {
            
        }
    }
}