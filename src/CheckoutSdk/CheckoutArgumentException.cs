using System;
using System.Runtime.Serialization;

namespace Checkout
{
    [Serializable]
    public sealed class CheckoutArgumentException : CheckoutException
    {
        public CheckoutArgumentException(string message) : base(message)
        {
        }

        private CheckoutArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static CheckoutArgumentException WithMessage(string message)
        {
            return new CheckoutArgumentException(message);
        }
    }
}