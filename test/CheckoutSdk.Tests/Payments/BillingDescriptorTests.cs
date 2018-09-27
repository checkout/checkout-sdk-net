using System;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class BillingDescriptorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenNameInvalidShouldThrowArgumentException(string invalidName)
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new BillingDescriptor(invalidName, "valid invalidCity"); }
            );

            validationException.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenCityInvalidShouldThrowArgumentException(string invalidCity)
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new BillingDescriptor("valid invalidName", invalidCity); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
