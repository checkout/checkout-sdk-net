using Checkout.Payments;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class PaymentRecipientTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("12345")]
        public void GivenAccountNumberInvalidShouldThrowArgumentException(string invalidAccountNumber)
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new PaymentRecipient(new DateTime(), invalidAccountNumber, "NW1", "Jones" ); }
            );

            validationException.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenZipInvalidShouldThrowArgumentException(string invalidZip)
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new PaymentRecipient(new DateTime(), "1234567890", invalidZip, "Jones"); }
            );

            validationException.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenLastNameInvalidShouldThrowArgumentException(string invalidLastName)
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new PaymentRecipient(new DateTime(), "1234567890", "NW1", invalidLastName); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
