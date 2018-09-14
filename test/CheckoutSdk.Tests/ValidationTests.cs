using System;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("test@@")]
        [InlineData("test@")]
        [InlineData("test@@test")]
        [InlineData("test")]
        public void CustomerSource_GivenEmailInvalidShouldThrowFormatException(string email)
        {
            var validationException = Should.Throw<FormatException>(
                () => { new CustomerSource(null, email: email); }
                );

            validationException.ShouldNotBeNull();            
        }

        [Fact]
        public void CustomerSource_GivenIdAndEmailIsMissingShouldThrowArgumentException()
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new CustomerSource(null, null); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
