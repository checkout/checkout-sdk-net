using System;
using System.Runtime.Serialization;

namespace Checkout
{
    [Serializable]
    public abstract class CheckoutException : Exception
    {
        protected CheckoutException(string message) : base(message)
        {
        }

        protected CheckoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        protected CheckoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
        
    }
}