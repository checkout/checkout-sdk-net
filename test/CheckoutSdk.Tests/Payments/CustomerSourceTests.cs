using System;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class CustomerSourceTests
    {
        [Theory]
        [InlineData("test@@")]
        [InlineData("test@")]
        [InlineData("test@@test")]
        [InlineData("test")]
        public void GivenEmailInvalidShouldThrowFormatException(string email)
        {
            var validationException = Should.Throw<FormatException>(
                () => { new CustomerSource(null, email: email); }
                );

            validationException.ShouldNotBeNull();            
        }

        [Fact]
        public void GivenIdAndEmailIsMissingShouldThrowArgumentException()
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new CustomerSource(null, null); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
