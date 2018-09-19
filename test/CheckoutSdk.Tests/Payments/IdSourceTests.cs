using System;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class IdSourceTests
    {
        [Fact]
        public void GivenIdMissingShouldThrowArgumentException()
        {
            var validationException = Should.Throw<ArgumentException>(
                () => { new IdSource(null, null); }
            );

            validationException.ShouldNotBeNull();
        }
    }
}
