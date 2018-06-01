using System;

namespace Checkout.Tests
{
    public class TestHelper
    {
        public static string GenerateRandomEmail()
        {
            return Guid.NewGuid().ToString("n") + "@checkout-sdk-net.com";
        }
    }
}