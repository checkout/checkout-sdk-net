using System;

namespace Checkout
{
    public class CheckoutAuthorizationException : ArgumentException
    {
        public CheckoutAuthorizationException(string message) : base(message)
        {
        }

        public static CheckoutAuthorizationException InvalidAuthorization(SdkAuthorizationType authorizationType)
        {
            return new CheckoutAuthorizationException(
                $"Operation does not support {authorizationType} authorization type");
        }
    }
}