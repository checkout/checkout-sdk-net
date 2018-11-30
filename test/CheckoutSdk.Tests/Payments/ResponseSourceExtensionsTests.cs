using Shouldly;
using Xunit;

namespace Checkout.Payments.Tests
{
    public class ResponseSourceExtensionsTests
    {
        [Fact]
        public void CanConvertToCardSource()
        {
            var source = new CardSourceResponse();
            var payment = new PaymentProcessed
            {
                Source = source
            };

            payment.Source.AsCard().ShouldBeOfType<CardSourceResponse>();
            payment.Source.AsCard().ShouldBe(source);
        }

        [Fact]
        public void CanConvertToAlternativePaymentSource()
        {
            var source = new AlternativePaymentSourceResponse();
            var payment = new PaymentProcessed
            {
                Source = source
            };

            payment.Source.AsAlternativePayment().ShouldBeOfType<AlternativePaymentSourceResponse>();
            payment.Source.AsAlternativePayment().ShouldBe(source);
        }
    }
}