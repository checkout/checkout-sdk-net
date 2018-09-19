using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class CardSourcePaymentsTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public CardSourcePaymentsTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task RequestNonThreeDsCardPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDs = false;

            PaymentResponse apiResponse = await _api.Payments.RequestAsync(paymentRequest);
            
            apiResponse.Payment.ShouldNotBeNull();
            apiResponse.Payment.Approved.ShouldBeTrue();
            apiResponse.Payment.Id.ShouldNotBeNullOrEmpty();
            apiResponse.Payment.ActionId.ShouldNotBeNullOrEmpty();
            apiResponse.Payment.Amount.ShouldBe(paymentRequest.Amount.Value);
            apiResponse.Payment.Currency.ShouldBe(paymentRequest.Currency);
            apiResponse.Payment.Reference.ShouldBe(paymentRequest.Reference);
            apiResponse.Payment.Customer.ShouldNotBeNull();
            apiResponse.Payment.Customer.Id.ShouldNotBeNullOrEmpty();
            apiResponse.Payment.Customer.Email.ShouldNotBeNullOrEmpty();
            apiResponse.Payment.CanCapture().ShouldBeTrue();
            apiResponse.Payment.CanVoid().ShouldBeTrue();
        }

        [Fact]
        public async Task RequestThreeDsCardPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDs = true;

            PaymentResponse apiResponse = await _api.Payments.RequestAsync(paymentRequest);

            apiResponse.IsPending.ShouldBe(true);
            var pending = apiResponse.Pending;

            pending.ShouldNotBeNull();

            pending.Id.ShouldNotBeNullOrEmpty();
            pending.Reference.ShouldBe(paymentRequest.Reference);
            pending.Customer.ShouldNotBeNull();
            pending.Customer.Id.ShouldNotBeNullOrEmpty();
            pending.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            pending.ThreeDs.ShouldNotBeNull();
            pending.ThreeDs.Downgraded.ShouldBe(false);
            //pending.ThreeDs.Enrolled.ShouldNotBeNullOrEmpty(); //todo uncomment after 2018-09-20
            pending.RequiresRedirect().ShouldBe(true);
            pending.GetRedirectLink().ShouldNotBeNull();
        }

        [Fact]
        public async Task ItCanCapturePayment()
        {
            // Auth
            var paymentRequest = TestHelper.CreateCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            CaptureRequest captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Capture
            var captureResponse = await _api.Payments.CaptureAsync(paymentResponse.Payment.Id, captureRequest);

            captureResponse.ActionId.ShouldNotBeNullOrEmpty();
            captureResponse.Reference.ShouldBe(captureRequest.Reference);
        }

        [Fact]
        public async Task ItCanVoidPayment()
        {
            // Auth
            var paymentRequest = TestHelper.CreateCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanVoid().ShouldBe(true);

            VoidRequest voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Void Auth
            var voidResponse = await _api.Payments.VoidAsync(paymentResponse.Payment.Id, voidRequest);

            voidResponse.ActionId.ShouldNotBeNullOrEmpty();
            voidResponse.Reference.ShouldBe(voidRequest.Reference);
        }

        [Fact]
        public async Task ItCanRefundPayment()
        {
            // Auth
            var paymentRequest = TestHelper.CreateCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            // Capture
            await _api.Payments.CaptureAsync(paymentResponse.Payment.Id);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Refund

            var refundResponse = await _api.Payments.RefundAsync(paymentResponse.Payment.Id, refundRequest);

            refundResponse.ActionId.ShouldNotBeNullOrEmpty();
            refundResponse.Reference.ShouldBe(refundRequest.Reference);
        }

        [Fact]
        public async Task ItCanGetNonThreeDsPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.ShouldNotBeNull();
            paymentDetails.Id.ShouldBe(paymentResponse.Payment.Id);
            paymentDetails.Customer.ShouldNotBeNull();
            paymentDetails.Customer.Id.ShouldBe(paymentResponse.Payment.Customer.Id);
            paymentDetails.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            paymentDetails.Amount.ShouldBe(paymentResponse.Payment.Amount);
            paymentDetails.Currency.ShouldBe(paymentResponse.Payment.Currency);
            paymentDetails.BillingDescriptor.ShouldNotBeNull();
            paymentDetails.PaymentType.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Reference.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Risk.ShouldNotBeNull();
            paymentDetails.RequestedOn.ShouldBeGreaterThan(paymentResponse.Payment.ProcessedOn.AddMinutes(-1));
            paymentDetails.ThreeDs.ShouldBeNull();
            paymentDetails.Links.ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeEmpty();
            paymentDetails.Status.ShouldBe("Authorized");
            paymentDetails.Source.AsCardSource().ShouldNotBeNull();
        }

        [Fact]
        public async Task ItCanGetThreeDsPaymentBeforeAuth()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDs = true;
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.IsPending.ShouldBe(true);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Pending.Id);

            paymentDetails.ShouldNotBeNull();
            paymentDetails.Id.ShouldBe(paymentResponse.Pending.Id);
            paymentDetails.Customer.ShouldNotBeNull();
            paymentDetails.Customer.Id.ShouldBe(paymentResponse.Pending.Customer.Id);
            paymentDetails.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            paymentDetails.Amount.ShouldBe(paymentRequest.Amount);
            paymentDetails.Currency.ShouldBe(paymentRequest.Currency); 
            paymentDetails.PaymentType.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Reference.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Risk.ShouldNotBeNull();
            paymentDetails.RequestedOn.ShouldBeGreaterThan(DateTime.MinValue);
            paymentDetails.ThreeDs.ShouldNotBeNull();
            paymentDetails.ThreeDs.Downgraded.ShouldBe(false);
            //paymentDetails.ThreeDs.Enrolled.ShouldNotBeNullOrEmpty(); //todo uncomment after 2018-09-20
            paymentDetails.RequiresRedirect().ShouldBe(true);
            paymentDetails.GetRedirectLink().ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeEmpty();
            paymentDetails.Status.ShouldBe("Pending");
            paymentDetails.Source.AsCardSource().ShouldNotBeNull();
        }

        [Fact]
        public async Task ItCanGetPaymentDestinations()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            var destination = new PaymentDestination("test", 1);
            paymentRequest.Destinations = new[] { destination };
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Destinations.ShouldNotBeNull();
            paymentDetails.Destinations.ShouldNotBeEmpty();
            paymentDetails.Destinations.ShouldHaveSingleItem();
            paymentDetails.Destinations.ShouldContain(d => d.Id == destination.Id && d.Amount == destination.Amount);
        }

        [Fact]
        public async Task ItCanGetPaymentMetadata()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            var metadata = new KeyValuePair<string, object>("test", "1234");
            paymentRequest.Metadata.Add(metadata.Key, metadata.Value);
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Metadata.ShouldNotBeNull();
            paymentDetails.Metadata.ShouldNotBeEmpty();
            paymentDetails.Metadata.ShouldHaveSingleItem();
            paymentDetails.Metadata.ShouldContain(d => d.Key == metadata.Key && d.Value.Equals(metadata.Value));
        }

        [Fact]
        public async Task ItCanGetPaymentIp()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.PaymentIp = "10.1.2.3";
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.PaymentIp.ShouldBe(paymentRequest.PaymentIp);
        }

        [Fact]
        public async Task ItCanGetPaymentRecipient()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Recipient = new PaymentRecipient()
            {
                AccountNumber = "5555554444",
                Dob = new DateTime(1985, 05, 15),
                LastName = "Wensleydale",
                Zip = "W1T"
            };
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Recipient.ShouldNotBeNull();
            paymentDetails.Recipient.AccountNumber.ShouldBe(paymentRequest.Recipient.AccountNumber);
            paymentDetails.Recipient.Dob.ShouldBe(paymentRequest.Recipient.Dob);
            paymentDetails.Recipient.LastName.ShouldBe(paymentRequest.Recipient.LastName.Take(6));
            paymentDetails.Recipient.Zip.ShouldBe(paymentRequest.Recipient.Zip);
        }

        [Fact]
        public async Task ItCanGetPaymentShipping()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Shipping = new ShippingDetails()
            {
                Address = new Address() { AddressLine1 = "221B Baker Street", AddressLine2 = null, City = "London", Country = "UK", State = "n/a", Zip = "NW1 6XE" },
                Phone = new Phone() { CountryCode = "44", Number = "124312431243" }
            };
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Shipping.ShouldNotBeNull();
            paymentDetails.Shipping.Address.ShouldNotBeNull();
            paymentDetails.Shipping.Address.AddressLine1.ShouldBe(paymentRequest.Shipping.Address.AddressLine1);
            paymentDetails.Shipping.Address.AddressLine2.ShouldBe(paymentRequest.Shipping.Address.AddressLine2);
            paymentDetails.Shipping.Address.City.ShouldBe(paymentRequest.Shipping.Address.City);
            paymentDetails.Shipping.Address.Country.ShouldBe(paymentRequest.Shipping.Address.Country);
            paymentDetails.Shipping.Address.State.ShouldBe(paymentRequest.Shipping.Address.State);
            paymentDetails.Shipping.Address.Zip.ShouldBe(paymentRequest.Shipping.Address.Zip);
            paymentDetails.Shipping.Phone.CountryCode.ShouldBe(paymentRequest.Shipping.Phone.CountryCode);
            paymentDetails.Shipping.Phone.Number.ShouldBe(paymentRequest.Shipping.Phone.Number);
        }

        [Fact]
        public async Task ItCanGetPaymentDescription()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Description = "Too descriptive";
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Description.ShouldBe(paymentRequest.Description);
        }
    }
}