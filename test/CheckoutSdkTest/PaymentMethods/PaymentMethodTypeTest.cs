using Checkout.PaymentMethods.Entities;
using Shouldly;
using Xunit;
using PaymentSourceType = Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.SourceType;

namespace Checkout.PaymentMethods
{
    public class PaymentMethodTypeTest
    {
        [Theory]
        [InlineData(PaymentSourceType.Card, 0)]
        [InlineData(PaymentSourceType.Afterpay, 1)]
        [InlineData(PaymentSourceType.PaymentResponseSource, 41)]
        public void ShouldPreserveExistingSourceTypeOrdinals(PaymentSourceType sourceType, int ordinal)
        {
            ((int)sourceType).ShouldBe(ordinal);
        }

        [Theory]
        [InlineData(PaymentMethodType.Bacs, "bacs")]
        [InlineData(PaymentMethodType.Blik, "blik")]
        public void ShouldMapNewPaymentMethodTypesToTheirWireValues(
            PaymentMethodType paymentMethodType,
            string wireValue)
        {
            CheckoutUtils.GetEnumMemberValue(paymentMethodType).ShouldBe(wireValue);
            CheckoutUtils.GetEnumFromStringMemberValue<PaymentMethodType>(wireValue)
                .ShouldBe(paymentMethodType);
        }
    }
}