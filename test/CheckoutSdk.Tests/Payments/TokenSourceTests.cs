using System;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class TokenSourceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenTokenInvalidShouldThrowArgumentException(string token)
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new TokenSource(token); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
