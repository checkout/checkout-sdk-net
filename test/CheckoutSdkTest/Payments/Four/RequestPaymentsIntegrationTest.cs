using System;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments.Four.Request;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Response.Source;
using Shouldly;
using Xunit;

namespace Checkout.Payments.Four
{
    public class RequestPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldMakeCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true, captureOn: DateTime.Now.AddDays(2));
            paymentResponse.ShouldNotBeNull();

            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.ProcessedOn.ShouldNotBeNull();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseCode.ShouldNotBeNullOrEmpty();
            paymentResponse.SchemeId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseSummary.ShouldBe("Approved");
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);
            paymentResponse.Amount.ShouldBe(10);
            paymentResponse.Approved.ShouldBe(true);
            paymentResponse.AuthCode.ShouldNotBeNullOrEmpty();
            paymentResponse.Currency.ShouldBe(Currency.USD);
            paymentResponse.ThreeDs.ShouldBeNull();
            //Source
            paymentResponse.Source.ShouldBeAssignableTo(typeof(CardResponseSource));
            var cardSourceResponse = (CardResponseSource)paymentResponse.Source;
            cardSourceResponse.Type().ShouldBe(PaymentSourceType.Card);
            cardSourceResponse.Id.ShouldNotBeNullOrEmpty();
            cardSourceResponse.AvsCheck.ShouldBe("G");
            cardSourceResponse.CvvCheck.ShouldBe("Y");
            cardSourceResponse.Bin.ShouldNotBeNull();
            cardSourceResponse.CardCategory.ShouldBe(CardCategory.Consumer);
            cardSourceResponse.CardType.ShouldBe(CardType.Credit);
            cardSourceResponse.ExpiryMonth.ShouldNotBeNull();
            cardSourceResponse.ExpiryYear.ShouldNotBeNull();
            cardSourceResponse.Last4.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Scheme.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Name.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Fingerprint.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.Issuer.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.IssuerCountry.ShouldBe(CountryCode.US);
            cardSourceResponse.ProductId.ShouldNotBeNullOrEmpty();
            cardSourceResponse.ProductType.ShouldNotBeNullOrEmpty();
            //Customer
            paymentResponse.Customer.ShouldNotBeNull();
            paymentResponse.Customer.Id.ShouldNotBeNull();
            paymentResponse.Customer.Name.ShouldNotBeNull();
            paymentResponse.Customer.Email.ShouldNotBeNull();
            //Processing
            paymentResponse.Processing.ShouldNotBeNull();
            paymentResponse.Processing.AcquirerTransactionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Processing.RetrievalReferenceNumber.ShouldNotBeNullOrEmpty();
            //Risk
            paymentResponse.Risk.Flagged.ShouldBe(false);
            //Balances
            paymentResponse.Balances.ShouldNotBeNull();
            paymentResponse.Balances.TotalAuthorized.ShouldBe(10);
            paymentResponse.Balances.TotalCaptured.ShouldBe(0);
            paymentResponse.Balances.TotalRefunded.ShouldBe(0);
            paymentResponse.Balances.TotalVoided.ShouldBe(0);
            paymentResponse.Balances.AvailableToCapture.ShouldBe(10);
            paymentResponse.Balances.AvailableToRefund.ShouldBe(0);
            paymentResponse.Balances.AvailableToVoid.ShouldBe(10);
            //Links
            paymentResponse.GetSelfLink().ShouldNotBeNull();
            paymentResponse.HasLink("actions").ShouldBeTrue();
            paymentResponse.HasLink("capture").ShouldBeTrue();
            paymentResponse.HasLink("void").ShouldBeTrue();
        }

        [Fact]
        private async Task ShouldMakeCardVerification()
        {
            var phone = new Phone {CountryCode = "44", Number = "020 222333"};

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var requestCardSource = new RequestCardSource()
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = false,
                Reference = Guid.NewGuid().ToString(),
                Amount = 0,
                Currency = Currency.USD,
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();

            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.ProcessedOn.ShouldNotBeNull();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseCode.ShouldNotBeNullOrEmpty();
            paymentResponse.SchemeId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseSummary.ShouldBe("Approved");
            paymentResponse.Status.ShouldBe(PaymentStatus.CardVerified);
            paymentResponse.Amount.ShouldBe(0);
            paymentResponse.Approved.ShouldBe(true);
            paymentResponse.AuthCode.ShouldNotBeNullOrEmpty();
            paymentResponse.Currency.ShouldBe(Currency.USD);
            paymentResponse.ThreeDs.ShouldBeNull();
            //Source
            paymentResponse.Source.ShouldBeAssignableTo(typeof(CardResponseSource));
            var cardSourceResponse = (CardResponseSource)paymentResponse.Source;
            cardSourceResponse.Type().ShouldBe(PaymentSourceType.Card);
            cardSourceResponse.Id.ShouldNotBeNullOrEmpty();
            cardSourceResponse.AvsCheck.ShouldBe("G");
            cardSourceResponse.CvvCheck.ShouldBe("Y");
            cardSourceResponse.Bin.ShouldNotBeNull();
            cardSourceResponse.CardCategory.ShouldBe(CardCategory.Consumer);
            cardSourceResponse.CardType.ShouldBe(CardType.Credit);
            cardSourceResponse.ExpiryMonth.ShouldNotBeNull();
            cardSourceResponse.ExpiryYear.ShouldNotBeNull();
            cardSourceResponse.Last4.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Scheme.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Name.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Fingerprint.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.Issuer.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.IssuerCountry.ShouldBe(CountryCode.US);
            cardSourceResponse.ProductId.ShouldNotBeNullOrEmpty();
            cardSourceResponse.ProductType.ShouldNotBeNullOrEmpty();
            //Customer
            paymentResponse.Customer.ShouldBeNull();
            //Processing
            paymentResponse.Processing.ShouldNotBeNull();
            paymentResponse.Processing.AcquirerTransactionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Processing.RetrievalReferenceNumber.ShouldNotBeNullOrEmpty();
            //Risk
            paymentResponse.Risk.Flagged.ShouldBe(false);
            //Balances
            paymentResponse.Balances.ShouldNotBeNull();
            paymentResponse.Balances.TotalAuthorized.ShouldBe(0);
            paymentResponse.Balances.TotalCaptured.ShouldBe(0);
            paymentResponse.Balances.TotalRefunded.ShouldBe(0);
            paymentResponse.Balances.TotalVoided.ShouldBe(0);
            paymentResponse.Balances.AvailableToCapture.ShouldBe(0);
            paymentResponse.Balances.AvailableToRefund.ShouldBe(0);
            paymentResponse.Balances.AvailableToVoid.ShouldBe(0);
            //Links
            paymentResponse.GetSelfLink().ShouldNotBeNull();
            paymentResponse.HasLink("actions").ShouldBeTrue();
            paymentResponse.HasLink("capture").ShouldBeFalse();
            paymentResponse.HasLink("void").ShouldBeFalse();
        }

        [Fact]
        private async Task ShouldMakeCard3dsPayment()
        {
            var paymentResponse = await Make3dsCardPayment();

            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            //3ds
            paymentResponse.ThreeDs.ShouldNotBeNull();
            paymentResponse.ThreeDs.Downgraded.ShouldBe(false);
            paymentResponse.ThreeDs.Enrolled.ShouldBe(ThreeDsEnrollmentStatus.Yes);
            //Customer
            paymentResponse.Customer.ShouldNotBeNull();
            paymentResponse.Customer.Id.ShouldNotBeNull();
            paymentResponse.Customer.Name.ShouldNotBeNull();
            paymentResponse.Customer.Email.ShouldNotBeNull();
            //Links
            paymentResponse.GetSelfLink().ShouldNotBeNull();
            paymentResponse.HasLink("redirect").ShouldBeTrue();
        }

        [Fact]
        private async Task ShouldMakeCard3dsPayment_N3d()
        {
            var paymentResponse = await Make3dsCardPayment(true);

            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.ProcessedOn.ShouldNotBeNull();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseCode.ShouldNotBeNullOrEmpty();
            paymentResponse.SchemeId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseSummary.ShouldBe("Approved");
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);
            paymentResponse.Amount.ShouldBe(10);
            paymentResponse.Approved.ShouldBe(true);
            paymentResponse.AuthCode.ShouldNotBeNullOrEmpty();
            paymentResponse.Currency.ShouldBe(Currency.USD);
            paymentResponse.ThreeDs.ShouldBeNull();
            //Source
            paymentResponse.Source.ShouldBeAssignableTo(typeof(CardResponseSource));
            var cardSourceResponse = (CardResponseSource)paymentResponse.Source;
            cardSourceResponse.Type().ShouldBe(PaymentSourceType.Card);
            cardSourceResponse.Id.ShouldNotBeNullOrEmpty();
            cardSourceResponse.AvsCheck.ShouldBe("G");
            cardSourceResponse.CvvCheck.ShouldBe("Y");
            cardSourceResponse.Bin.ShouldNotBeNull();
            cardSourceResponse.CardCategory.ShouldBe(CardCategory.Consumer);
            cardSourceResponse.CardType.ShouldBe(CardType.Credit);
            cardSourceResponse.ExpiryMonth.ShouldNotBeNull();
            cardSourceResponse.ExpiryYear.ShouldNotBeNull();
            cardSourceResponse.Last4.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Scheme.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Name.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Fingerprint.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.Issuer.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.IssuerCountry.ShouldBe(CountryCode.US);
            cardSourceResponse.ProductId.ShouldNotBeNullOrEmpty();
            cardSourceResponse.ProductType.ShouldNotBeNullOrEmpty();
            //Customer
            paymentResponse.Customer.ShouldNotBeNull();
            paymentResponse.Customer.Id.ShouldNotBeNull();
            paymentResponse.Customer.Name.ShouldNotBeNull();
            //Processing
            paymentResponse.Processing.ShouldNotBeNull();
            paymentResponse.Processing.AcquirerTransactionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Processing.RetrievalReferenceNumber.ShouldNotBeNullOrEmpty();
            //Risk
            paymentResponse.Risk.Flagged.ShouldBe(false);
            //Links
            paymentResponse.GetSelfLink().ShouldNotBeNull();
            paymentResponse.HasLink("actions").ShouldBeTrue();
            paymentResponse.HasLink("capture").ShouldBeTrue();
            paymentResponse.HasLink("void").ShouldBeTrue();
        }

        [Fact]
        private async Task ShouldTokenPayment()
        {
            var paymentResponse = await MakeTokenPayment();

            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.ProcessedOn.ShouldNotBeNull();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseCode.ShouldNotBeNullOrEmpty();
            paymentResponse.SchemeId.ShouldNotBeNullOrEmpty();
            paymentResponse.ResponseSummary.ShouldBe("Approved");
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);
            paymentResponse.Amount.ShouldBe(10);
            paymentResponse.Approved.ShouldBe(true);
            paymentResponse.AuthCode.ShouldNotBeNullOrEmpty();
            paymentResponse.Currency.ShouldBe(Currency.USD);
            paymentResponse.ThreeDs.ShouldBeNull();
            //Source
            paymentResponse.Source.ShouldBeAssignableTo(typeof(CardResponseSource));
            var cardSourceResponse = (CardResponseSource)paymentResponse.Source;
            cardSourceResponse.Type().ShouldBe(PaymentSourceType.Card);
            cardSourceResponse.Id.ShouldNotBeNullOrEmpty();
            cardSourceResponse.AvsCheck.ShouldBe("G");
            cardSourceResponse.CvvCheck.ShouldBe("Y");
            cardSourceResponse.Bin.ShouldNotBeNull();
            cardSourceResponse.CardCategory.ShouldBe(CardCategory.Consumer);
            cardSourceResponse.CardType.ShouldBe(CardType.Credit);
            cardSourceResponse.ExpiryMonth.ShouldNotBeNull();
            cardSourceResponse.ExpiryYear.ShouldNotBeNull();
            cardSourceResponse.Last4.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Scheme.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Name.ShouldNotBeNullOrEmpty();
            cardSourceResponse.Fingerprint.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.Issuer.ShouldNotBeNullOrEmpty();
            //cardSourceResponse.IssuerCountry.ShouldBe(CountryCode.US);
            cardSourceResponse.ProductId.ShouldNotBeNullOrEmpty();
            cardSourceResponse.ProductType.ShouldNotBeNullOrEmpty();
            //Customer
            paymentResponse.Customer.ShouldNotBeNull();
            paymentResponse.Customer.Id.ShouldNotBeNull();
            paymentResponse.Customer.Name.ShouldBeNull();
            //Processing
            paymentResponse.Processing.ShouldNotBeNull();
            paymentResponse.Processing.AcquirerTransactionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Processing.RetrievalReferenceNumber.ShouldNotBeNullOrEmpty();
            //Risk
            paymentResponse.Risk.Flagged.ShouldBe(false);
            //Links
            paymentResponse.GetSelfLink().ShouldNotBeNull();
            paymentResponse.HasLink("actions").ShouldBeTrue();
            paymentResponse.HasLink("capture").ShouldBeTrue();
            paymentResponse.HasLink("void").ShouldBeTrue();
        }

        [Fact]
        private async Task ShouldMakePaymentsIdempotently()
        {
            var phone = new Phone {CountryCode = "44", Number = "020 222333"};

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = false,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10,
                Currency = Currency.USD
            };

            var paymentResponse1 = await FourApi.PaymentsClient().RequestPayment(paymentRequest, IdempotencyKey);
            paymentResponse1.ShouldNotBeNull();

            var paymentResponse2 = await FourApi.PaymentsClient().RequestPayment(paymentRequest, IdempotencyKey);
            paymentResponse2.ShouldNotBeNull();

            paymentResponse1.Id.ShouldBe(paymentResponse2.Id);
        }
    }
}