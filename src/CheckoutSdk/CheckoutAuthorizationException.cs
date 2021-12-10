using System;
using System.Runtime.Serialization;

namespace Checkout
{
    [Serializable]
    public sealed class CheckoutAuthorizationException : CheckoutException
    {
        public CheckoutAuthorizationException(string message) : base(message)
        {
        }

        public CheckoutAuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private CheckoutAuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static CheckoutAuthorizationException InvalidAuthorization(SdkAuthorizationType authorizationType)
        {
            return new CheckoutAuthorizationException(
                $"Operation does not support {authorizationType} authorization type");
        }
    }
}