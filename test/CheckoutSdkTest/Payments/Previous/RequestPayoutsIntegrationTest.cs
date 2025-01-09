using Checkout.Common;
using Checkout.Payments.Previous.Request;
using Checkout.Payments.Previous.Request.Destination;
using Checkout.Payments.Previous.Response.Destination;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class RequestPayoutsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldRequestPayout()
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

            var requestCardDestination = new PaymentRequestCardDestination
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                FirstName = "Mr. Test",
                LastName = "Integration",
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var payoutRequest = new PayoutRequest
            {
                Destination = requestCardDestination,
                FundTransferType = FundTransferType.AA,
                Capture = false,
                Reference = Guid.NewGuid().ToString(),
                Amount = 5,
                Currency = Currency.USD,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayout(payoutRequest);
            paymentResponse.ShouldNotBeNull();

            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ThreeDs.ShouldBeNull();
            //Customer
            paymentResponse.Customer.ShouldNotBeNull();
            paymentResponse.Customer.Id.ShouldNotBeNull();
            paymentResponse.Customer.Name.ShouldBeNull();
            //Links
            paymentResponse.GetSelfLink().ShouldNotBeNull();
            paymentResponse.HasLink("actions").ShouldBeFalse();
            paymentResponse.HasLink("capture").ShouldBeFalse();
            paymentResponse.HasLink("void").ShouldBeFalse();

            var payment = await Retriable(async () =>
                await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id));
            payment.ShouldNotBeNull();

            // Destination
            payment.Destination.ShouldNotBeNull();
            payment.Destination.ShouldBeAssignableTo(typeof(PaymentResponseCardDestination));
            var paymentResponseCardDestination = (PaymentResponseCardDestination)payment.Destination;
            paymentResponseCardDestination.Bin.ShouldNotBeNull();
            //paymentResponseCardDestination.CardCategory.ShouldBe(CardCategory.Consumer);
            //paymentResponseCardDestination.CardType.ShouldBe(CardType.Credit);
            paymentResponseCardDestination.ExpiryMonth.ShouldBe(6);
            paymentResponseCardDestination.ExpiryYear.ShouldBe(2025);
            paymentResponseCardDestination.Last4.ShouldNotBeNullOrEmpty();
            paymentResponseCardDestination.Fingerprint.ShouldNotBeNullOrEmpty();
            paymentResponseCardDestination.Name.ShouldNotBeNullOrEmpty();
            //paymentResponseCardDestination.Issuer.ShouldNotBeNullOrEmpty();
            //paymentResponseCardDestination.IssuerCountry.ShouldBe(CountryCode.US);
            //paymentResponseCardDestination.ProductId.ShouldNotBeNullOrEmpty();
            //paymentResponseCardDestination.ProductType.ShouldNotBeNullOrEmpty();
            paymentResponseCardDestination.Type().ShouldBe(PaymentDestinationType.Card);
        }
    }
}