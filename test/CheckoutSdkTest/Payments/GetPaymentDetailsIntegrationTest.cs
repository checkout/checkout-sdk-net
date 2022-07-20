using Checkout.Common;
using Checkout.Payments.Response;
using Checkout.Payments.Response.Source;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class GetPaymentDetailsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldGetPaymentDetails()
        {
            var paymentResponse = await MakeCardPayment(true);

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id), PaymentIsCaptured);

            payment.Id.ShouldNotBeNullOrEmpty();
            payment.PaymentType.ShouldBe(PaymentType.Regular);
            payment.RequestedOn.ShouldNotBeNull();
            payment.Reference.ShouldNotBeNullOrEmpty();
            payment.SchemeId.ShouldNotBeNullOrEmpty();
            payment.Status.ShouldBe(PaymentStatus.Captured);
            payment.Amount.ShouldBe(10);
            payment.Approved.ShouldBe(true);
            payment.Currency.ShouldBe(Currency.USD);
            payment.ThreeDs.ShouldBeNull();
            payment.SchemeId.ShouldNotBeNullOrEmpty();
            payment.Eci.ShouldBeNull();
            //Source
            payment.Source.ShouldBeAssignableTo(typeof(CardResponseSource));
            var cardSourcePayment = (CardResponseSource)payment.Source;
            cardSourcePayment.Type().ShouldBe(PaymentSourceType.Card);
            cardSourcePayment.Id.ShouldNotBeNullOrEmpty();
            cardSourcePayment.AvsCheck.ShouldBe("G");
            cardSourcePayment.CvvCheck.ShouldBe("Y");
            cardSourcePayment.Bin.ShouldNotBeNull();
            cardSourcePayment.CardCategory.ShouldBe(CardCategory.Consumer);
            cardSourcePayment.CardType.ShouldBe(CardType.Credit);
            cardSourcePayment.ExpiryMonth.ShouldNotBeNull();
            cardSourcePayment.ExpiryYear.ShouldNotBeNull();
            cardSourcePayment.Last4.ShouldNotBeNullOrEmpty();
            cardSourcePayment.Scheme.ShouldNotBeNullOrEmpty();
            cardSourcePayment.Name.ShouldNotBeNullOrEmpty();
            cardSourcePayment.Fingerprint.ShouldNotBeNullOrEmpty();
            //cardSourcePayment.Issuer.ShouldNotBeNullOrEmpty();
            //cardSourcePayment.IssuerCountry.ShouldBe(CountryCode.US);
            cardSourcePayment.ProductId.ShouldNotBeNullOrEmpty();
            cardSourcePayment.ProductType.ShouldNotBeNullOrEmpty();
            //Customer
            payment.Customer.ShouldNotBeNull();
            payment.Customer.Id.ShouldNotBeNull();
            payment.Customer.Name.ShouldNotBeNull();
            payment.Customer.Email.ShouldNotBeNull();
            //Risk
            payment.Risk.Flagged.ShouldBe(false);
            //Links
            payment.GetSelfLink().ShouldNotBeNull();
            payment.HasLink("actions").ShouldBeTrue();
            payment.HasLink("capture").ShouldBeFalse();
            payment.HasLink("void").ShouldBeFalse();
        }

        private static bool PaymentIsCaptured(GetPaymentResponse obj)
        {
            return obj.Status == PaymentStatus.Captured;
        }
    }
}