using Checkout.Common;
using Checkout.Payments.Request;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class RefundPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldRefundCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true);
            var order = new RefundOrder() { Name = "OrderTest", TotalAmount = 99, Quantity = 88 };
            var bank = new BankDetails { Name = "Lloyds TSB", Branch = "Bournemouth", Address = GetAddress() };
            var destination = new Destination
            {
                AccountType = AccountType.Savings,
                AccountNumber = "13654567455",
                BankCode = "23-456",
                BranchCode = "6443",
                Iban = "HU93116000060000000012345676",
                Bban = "3704 0044 0532 0130 00",
                SwiftBic = "37040044",
                Country = CountryCode.GB,
                AccountHolder = GetAccountHolder(),
                Bank = bank
            };

            var refundRequest = new RefundRequest
            {
                Amount = paymentResponse.Amount,
                Reference = Guid.NewGuid().ToString(),
                Items = new List<RefundOrder> { order },
                Destination = destination
            };

            var response = await Retriable(async () =>
                await DefaultApi.PaymentsClient().RefundPayment(paymentResponse.Id, refundRequest));

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id));
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(0);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        [Fact]
        private async Task ShouldRefundPartiallyCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true);

            var refundRequest = new RefundRequest
            {
                Amount = paymentResponse.Amount / 2, Reference = Guid.NewGuid().ToString(),
            };

            var response = await Retriable(async () =>
                await DefaultApi.PaymentsClient().RefundPayment(paymentResponse.Id, refundRequest));

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id));
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }
    }
}