using System;

namespace Checkout.Sdk.Tests
{
    public class TestHelper
    {
        public static string GenerateRandomEmail()
        {
            return Guid.NewGuid().ToString("n") + "@checkout-sdk-net.com";
        }
    }
}