using System;
using System.Runtime.Serialization;

namespace Checkout
{
    [Serializable]
    public sealed class CheckoutFileException : CheckoutException
    {
        public CheckoutFileException(string message) : base(message)
        {
        }

        private CheckoutFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}