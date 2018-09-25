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
        public async Task CanRequestNonThreeDsCardPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDs = false;

            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            
            paymentResponse.Payment.ShouldNotBeNull();
            paymentResponse.Payment.Approved.ShouldBeTrue();
            paymentResponse.Payment.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Amount.ShouldBe(paymentRequest.Amount.Value);
            paymentResponse.Payment.Currency.ShouldBe(paymentRequest.Currency);
            paymentResponse.Payment.Reference.ShouldBe(paymentRequest.Reference);
            paymentResponse.Payment.Customer.ShouldNotBeNull();
            paymentResponse.Payment.Customer.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Customer.Email.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.CanCapture().ShouldBeTrue();
            paymentResponse.Payment.CanVoid().ShouldBeTrue();
            paymentResponse.Payment.Source.AsCardSource().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanRequestThreeDsCardPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDs = true;

            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            paymentResponse.IsPending.ShouldBe(true);
            var pending = paymentResponse.Pending;

            pending.ShouldNotBeNull();

            pending.Id.ShouldNotBeNullOrEmpty();
            pending.Reference.ShouldBe(paymentRequest.Reference);
            pending.Customer.ShouldNotBeNull();
            pending.Customer.Id.ShouldNotBeNullOrEmpty();
            pending.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            pending.ThreeDs.ShouldNotBeNull();
            pending.ThreeDs.Downgraded.ShouldBe(false);
            pending.ThreeDs.Enrolled.ShouldNotBeNullOrEmpty();
            pending.RequiresRedirect().ShouldBe(true);
            pending.GetRedirectLink().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanCapturePayment()
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
        public async Task CanVoidPayment()
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
        public async Task CanRefundPayment()
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
        public async Task CanGetNonThreeDsPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.ShouldNotBeNull();
            paymentDetails.Id.ShouldBe(paymentResponse.Payment.Id);
            paymentDetails.Customer.ShouldNotBeNull();
            paymentDetails.Customer.Id.ShouldBe(paymentResponse.Payment.Customer.Id);
            paymentDetails.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            paymentDetails.Amount.ShouldBe(paymentResponse.Payment.Amount);
            paymentDetails.Currency.ShouldBe(paymentResponse.Payment.Currency);
            paymentDetails.BillingDescriptor.ShouldNotBeNull();
            paymentDetails.PaymentType.ShouldNotBeNull();
            paymentDetails.Reference.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Risk.ShouldNotBeNull();
            paymentDetails.RequestedOn.ShouldBeGreaterThan(paymentResponse.Payment.ProcessedOn.AddMinutes(-1));
            paymentDetails.ThreeDs.ShouldBeNull();
            paymentDetails.Links.ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeEmpty();
            paymentDetails.Status.ShouldBe(PaymentStatus.Authorized);
            paymentDetails.Source.AsCardSource().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanGetThreeDsPaymentBeforeAuth()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDs = true;
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.IsPending.ShouldBe(true);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Pending.Id);

            paymentDetails.ShouldNotBeNull();
            paymentDetails.Id.ShouldBe(paymentResponse.Pending.Id);
            paymentDetails.Customer.ShouldNotBeNull();
            paymentDetails.Customer.Id.ShouldBe(paymentResponse.Pending.Customer.Id);
            paymentDetails.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            paymentDetails.Amount.ShouldBe(paymentRequest.Amount);
            paymentDetails.Currency.ShouldBe(paymentRequest.Currency); 
            paymentDetails.PaymentType.ShouldNotBeNull();
            paymentDetails.Reference.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Risk.ShouldNotBeNull();
            paymentDetails.RequestedOn.ShouldBeGreaterThan(DateTime.MinValue);
            paymentDetails.ThreeDs.ShouldNotBeNull();
            paymentDetails.ThreeDs.Downgraded.ShouldBe(false);
            paymentDetails.ThreeDs.Enrolled.ShouldNotBeNullOrEmpty();
            paymentDetails.RequiresRedirect().ShouldBe(true);
            paymentDetails.GetRedirectLink().ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeEmpty();
            paymentDetails.Status.ShouldBe(PaymentStatus.Pending);
            paymentDetails.Source.AsCardSource().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanGetPaymentDestinations()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            var destination = new PaymentDestination("test", 1);
            paymentRequest.Destinations = new[] { destination };
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Destinations.ShouldNotBeNull();
            paymentDetails.Destinations.ShouldNotBeEmpty();
            paymentDetails.Destinations.ShouldHaveSingleItem();
            paymentDetails.Destinations.ShouldContain(d => d.Id == destination.Id && d.Amount == destination.Amount);
        }

        [Fact]
        public async Task CanGetPaymentMetadata()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            var metadata = new KeyValuePair<string, object>("test", "1234");
            paymentRequest.Metadata.Add(metadata.Key, metadata.Value);
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Metadata.ShouldNotBeNull();
            paymentDetails.Metadata.ShouldNotBeEmpty();
            paymentDetails.Metadata.ShouldHaveSingleItem();
            paymentDetails.Metadata.ShouldContain(d => d.Key == metadata.Key && d.Value.Equals(metadata.Value));
        }

        [Fact]
        public async Task CanGetPaymentIp()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.PaymentIp = "10.1.2.3";
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.PaymentIp.ShouldBe(paymentRequest.PaymentIp);
        }

        [Fact]
        public async Task CanGetPaymentRecipient()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Recipient =
                new PaymentRecipient(new DateTime(1985, 05, 15), "5555554444", "W1T", "Wensleydale");
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Recipient.ShouldNotBeNull();
            paymentDetails.Recipient.AccountNumber.ShouldBe(paymentRequest.Recipient.AccountNumber);
            paymentDetails.Recipient.Dob.ShouldBe(paymentRequest.Recipient.Dob);
            paymentDetails.Recipient.LastName.ShouldBe(paymentRequest.Recipient.LastName.Take(6));
            paymentDetails.Recipient.Zip.ShouldBe(paymentRequest.Recipient.Zip);
        }

        [Fact]
        public async Task CanGetPaymentShipping()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Shipping = new Shipping()
            {
                Address = new Address() { AddressLine1 = "221B Baker Street", AddressLine2 = null, City = "London", Country = "UK", State = "n/a", Zip = "NW1 6XE" },
                Phone = new Phone() { CountryCode = "44", Number = "124312431243" }
            };
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

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
        public async Task CanGetPaymentDescription()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Description = "Too descriptive";
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentDetailsResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Description.ShouldBe(paymentRequest.Description);
        }

        [Fact]
        public async Task CanGetPaymentAction()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            IEnumerable<PaymentAction> actionsResponse = await _api.Payments.GetActionsAsync(paymentResponse.Payment.Id);

            actionsResponse.ShouldNotBeNull();
            actionsResponse.ShouldHaveSingleItem();

            PaymentAction paymentAction = actionsResponse.SingleOrDefault();
            paymentAction.ShouldNotBeNull();
            paymentAction.Id.ShouldBe(paymentResponse.Payment.ActionId);
            paymentAction.ProcessedOn.ShouldBeGreaterThanOrEqualTo(paymentResponse.Payment.ProcessedOn);
            paymentAction.ResponseCode.ShouldBe(paymentResponse.Payment.ResponseCode);
            paymentAction.ResponseSummary.ShouldBe(paymentResponse.Payment.ResponseSummary);
            paymentAction.Reference.ShouldBe(paymentResponse.Payment.Reference);
            paymentAction.AuthCode.ShouldBe(paymentResponse.Payment.AuthCode);
            paymentAction.Type.ShouldBe(ActionType.Authorization);
            paymentAction.Links.ShouldNotBeNull();
        }

        [Fact]
        public async Task CanGetMultiplePaymentActions()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString()
            };
            CaptureResponse captureResponse = await _api.Payments.CaptureAsync(paymentResponse.Payment.Id, captureRequest);

            IEnumerable<PaymentAction> actionsResponse = await _api.Payments.GetActionsAsync(paymentResponse.Payment.Id);

            actionsResponse.ShouldNotBeNull();

            PaymentAction authorizationPaymentAction = actionsResponse.SingleOrDefault(a => a.Type == ActionType.Authorization);
            authorizationPaymentAction.ShouldNotBeNull();
            authorizationPaymentAction.Id.ShouldBe(paymentResponse.Payment.ActionId);

            PaymentAction capturePaymentAction = actionsResponse.SingleOrDefault(a => a.Type == ActionType.Capture);
            capturePaymentAction.ShouldNotBeNull();
            capturePaymentAction.Id.ShouldBe(captureResponse.ActionId);
            capturePaymentAction.Reference.ShouldBe(captureResponse.Reference);
            capturePaymentAction.Links.ShouldNotBeNull();
        }

        [Fact]
        public async Task CanGetPaymentActionMetadata()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            var metadata = new KeyValuePair<string, object>("test", "1234");
            paymentRequest.Metadata.Add(metadata.Key, metadata.Value);
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            IEnumerable<PaymentAction> actionsResponse = await _api.Payments.GetActionsAsync(paymentResponse.Payment.Id);

            actionsResponse.ShouldNotBeNull();

            PaymentAction paymentAction = actionsResponse.FirstOrDefault();
            paymentAction.ShouldNotBeNull();
            paymentAction.Metadata.ShouldNotBeNull();
            paymentAction.Metadata.ShouldNotBeEmpty();
            paymentAction.Metadata.ShouldHaveSingleItem();
            paymentAction.Metadata.ShouldContain(d => d.Key == metadata.Key && d.Value.Equals(metadata.Value));
        }
    }
}