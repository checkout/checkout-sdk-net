using System;
using Checkout.Sdk.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Sdk.Tests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("test")]
        public void CustomerSource_ThrowExceptionIfSourceEmailDoesNotHaveAtSign(string email)
        {
            var validationException = Should.Throw<CheckoutException>(
                () => { new CustomerSource(null, email: email); }
                );

            validationException.ShouldNotBeNull();            
        }

        [Fact]
        public void CustomerSource_ThrowExceptionIfNeitherIdNorEmailIsProvided()
        {
            var validationException = Should.Throw<CheckoutException>(
                () => { new CustomerSource(null, null); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
