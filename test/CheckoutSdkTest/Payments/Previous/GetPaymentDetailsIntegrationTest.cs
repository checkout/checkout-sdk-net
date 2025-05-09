using Checkout.Common;
using Checkout.Payments.Previous.Response.Source;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class GetPaymentDetailsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldGetPaymentDetails()
        {
            var paymentResponse = await MakeCardPayment(true);

            var payment = await Retriable(async () =>
                await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id));

            payment.Id.ShouldNotBeNullOrEmpty();
            payment.PaymentType.ShouldBe(PaymentType.Regular);
            payment.RequestedOn.ShouldNotBeNull();
            payment.Reference.ShouldNotBeNullOrEmpty();
            payment.SchemeId.ShouldNotBeNullOrEmpty();
            //payment.Status.ShouldBe(PaymentStatus.Captured);
            payment.Amount.ShouldBe(10);
            payment.Approved.ShouldBe(true);
            payment.Currency.ShouldBe(Currency.GBP);
            payment.ThreeDs.ShouldBeNull();
            //payment.Eci.ShouldNotBeNullOrEmpty();
            //Source
            payment.Source.ShouldBeAssignableTo(typeof(ResponseCardSource));
            var cardSourcePayment = (ResponseCardSource)payment.Source;
            cardSourcePayment.Type().ShouldBe(PaymentSourceType.Card);
            cardSourcePayment.Id.ShouldNotBeNullOrEmpty();
            cardSourcePayment.AvsCheck.ShouldBe("S");
            cardSourcePayment.CvvCheck.ShouldBe("Y");
            cardSourcePayment.Bin.ShouldNotBeNull();
            //cardSourcePayment.CardCategory.ShouldBe(CardCategory.Consumer);
            //cardSourcePayment.CardType.ShouldBe(CardType.Credit);
            cardSourcePayment.ExpiryMonth.ShouldNotBeNull();
            cardSourcePayment.ExpiryYear.ShouldNotBeNull();
            cardSourcePayment.Last4.ShouldNotBeNullOrEmpty();
            cardSourcePayment.Scheme.ShouldNotBeNullOrEmpty();
            cardSourcePayment.Name.ShouldNotBeNullOrEmpty();
            cardSourcePayment.FastFunds.ShouldNotBeNullOrEmpty();
            cardSourcePayment.Fingerprint.ShouldNotBeNullOrEmpty();
            //cardSourcePayment.Issuer.ShouldNotBeNullOrEmpty();
            //cardSourcePayment.IssuerCountry.ShouldBe(CountryCode.US);
            cardSourcePayment.Payouts.ShouldBe(true);
            //cardSourcePayment.ProductId.ShouldNotBeNullOrEmpty();
            //cardSourcePayment.ProductType.ShouldNotBeNullOrEmpty();
            //Customer
            payment.Customer.ShouldNotBeNull();
            payment.Customer.Id.ShouldNotBeNull();
            payment.Customer.Name.ShouldNotBeNull();
            //Risk
            payment.Risk.Flagged.ShouldBe(false);
            //Links
            payment.GetSelfLink().ShouldNotBeNull();
            payment.HasLink("actions").ShouldBeTrue();
            //payment.HasLink("capture").ShouldBeFalse();
            //payment.HasLink("void").ShouldBeFalse();
            //Other
            payment.BillingDescriptor.ShouldNotBeNull();
        }
    }
}