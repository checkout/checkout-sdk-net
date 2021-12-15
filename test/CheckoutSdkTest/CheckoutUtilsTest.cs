using Checkout.Common;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout
{
    public class CheckoutUtilsTest : UnitTestFixture
    {
        [Fact]
        private void ShouldGetEnumStringFromMemberValue()
        {
            CheckoutUtils.GetEnumFromStringMemberValue<PaymentDestinationType>("token")
                .ShouldBe(PaymentDestinationType.Token);
            CheckoutUtils.GetEnumFromStringMemberValue<ActionType>("Card Verification")
                .ShouldBe(ActionType.CardVerification);
            CheckoutUtils.GetEnumFromStringMemberValue<PaymentType>("XYZ")
                .ShouldBe(PaymentType.Regular);
        }
    }
}