using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class GetPaymentTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public GetPaymentTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }
        
        [Fact]
        public async Task CanGetNonThreeDsPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.PaymentType = PaymentType.Recurring;
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.ShouldNotBeNull();
            paymentDetails.Id.ShouldBe(paymentResponse.Payment.Id);
            paymentDetails.Customer.ShouldNotBeNull();
            paymentDetails.Customer.Id.ShouldBe(paymentResponse.Payment.Customer.Id);
            paymentDetails.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            paymentDetails.Amount.ShouldBe(paymentResponse.Payment.Amount);
            paymentDetails.Currency.ShouldBe(paymentResponse.Payment.Currency);
            paymentDetails.PaymentType.ShouldBe(paymentRequest.PaymentType);
            paymentDetails.BillingDescriptor.ShouldNotBeNull();
            paymentDetails.Reference.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.Risk.ShouldNotBeNull();
            paymentDetails.RequestedOn.ShouldBeGreaterThan(paymentResponse.Payment.ProcessedOn.AddMinutes(-1));
            paymentDetails.ThreeDS.ShouldBeNull();
            paymentDetails.Links.ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeEmpty();
            paymentDetails.Status.ShouldBe(PaymentStatus.Authorized);
            paymentDetails.Source.AsCard().ShouldNotBeNull();
            paymentDetails.Approved.ShouldBeTrue();
        }

        [Fact]
        public async Task CanGetThreeDsPaymentBeforeAuth()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDS = true;
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
            paymentDetails.Reference.ShouldNotBeNullOrWhiteSpace();
            paymentDetails.PaymentType.ShouldBe(PaymentType.Regular);
            paymentDetails.Risk.ShouldNotBeNull();
            paymentDetails.RequestedOn.ShouldBeGreaterThan(DateTime.MinValue);
            paymentDetails.ThreeDS.ShouldNotBeNull();
            paymentDetails.ThreeDS.Downgraded.ShouldBe(false);
            paymentDetails.ThreeDS.Enrolled.ShouldNotBeNullOrEmpty();
            paymentDetails.RequiresRedirect().ShouldBe(true);
            paymentDetails.GetRedirectLink().ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeNull();
            paymentDetails.Links.ShouldNotBeEmpty();
            paymentDetails.Status.ShouldBe(PaymentStatus.Pending);
            paymentDetails.Source.AsCard().ShouldNotBeNull();
            paymentDetails.Approved.ShouldBeFalse();
        }

        [Fact]
        public async Task CanGetPaymentMetadata()
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
        public async Task CanGetPaymentIp()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.PaymentIp = "10.1.2.3";
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.PaymentIp.ShouldBe(paymentRequest.PaymentIp);
        }

        [Fact]
        public async Task CanGetPaymentRecipient()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Recipient =
                new PaymentRecipient(new DateTime(1985, 05, 15), "4242424242", "W1T", lastName: "Wensle");
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

            paymentDetails.Recipient.ShouldNotBeNull();
            paymentDetails.Recipient.AccountNumber.ShouldBe(paymentRequest.Recipient.AccountNumber);
            paymentDetails.Recipient.DateOfBirth.ShouldBe(paymentRequest.Recipient.DateOfBirth);
            paymentDetails.Recipient.LastName.ShouldBe(paymentRequest.Recipient.LastName);
            paymentDetails.Recipient.Zip.ShouldBe(paymentRequest.Recipient.Zip);
        }

        [Fact]
        public async Task CanGetPaymentShipping()
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
        public async Task CanGetPaymentDescription()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.Description = "Too descriptive";
            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            GetPaymentResponse paymentDetails = await _api.Payments.GetAsync(paymentResponse.Payment.Id);

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

            PaymentProcessed payment = paymentResponse.Payment;
            PaymentAction paymentAction = actionsResponse.SingleOrDefault();
            paymentAction.ShouldNotBeNull();
            paymentAction.Id.ShouldBe(payment.ActionId);
            paymentAction.ProcessedOn.ShouldBeGreaterThanOrEqualTo(payment.ProcessedOn);
            paymentAction.Approved.ShouldBeTrue();
            paymentAction.Approved.ShouldBe(payment.Approved);
            paymentAction.ResponseCode.ShouldBe(payment.ResponseCode);
            paymentAction.ResponseSummary.ShouldBe(payment.ResponseSummary);
            paymentAction.Reference.ShouldBe(payment.Reference);
            paymentAction.AuthCode.ShouldBe(payment.AuthCode);
            paymentAction.Type.ShouldBe(ActionType.Authorization);
            paymentAction.Links.ShouldNotBeNull();
            paymentAction.Processing.ShouldNotBeNull();
            paymentAction.Processing.AcquirerTransactionId.ShouldNotBeNullOrWhiteSpace();
            paymentAction.Processing.RetrievalReferenceNumber.ShouldNotBeNullOrWhiteSpace();
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
            capturePaymentAction.Processing.AcquirerReferenceNumber.ShouldNotBeNullOrWhiteSpace();
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