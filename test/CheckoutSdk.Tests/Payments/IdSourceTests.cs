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
                () => { new IdSource(null); }
            );

            validationException.ShouldNotBeNull();
        }

        [Fact]
        public void CanCreateIdSource()
        {
            var source = new IdSource("src_xxx") { Cvv = "0757" };
            source.Id.ShouldBe("src_xxx");
            source.Cvv.ShouldBe("0757");
        }
    }
}
