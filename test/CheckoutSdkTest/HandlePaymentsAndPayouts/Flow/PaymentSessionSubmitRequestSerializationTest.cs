using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.Payments;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;
using PaymentInstruction = Checkout.Payments.PaymentInstruction;
using PaymentMethodConfiguration = Checkout.HandlePaymentsAndPayouts.Flow.Entities.PaymentMethodConfiguration;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    public class PaymentSessionSubmitRequestSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "token_abc123"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllNewFields()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "token_abc123",
                Amount = 1000,
                Currency = Currency.GBP,
                Reference = "ORD-123",
                ProcessingChannelId = "pc_q4laqzbdu2uerpha4xeneqbe2q",
                Capture = true,
                CaptureOn = new DateTime(2026, 6, 1, 9, 0, 0, DateTimeKind.Utc),
                Billing = new BillingInformation
                {
                    Address = new Address
                    {
                        AddressLine1 = "123 High St.",
                        City = "London",
                        Zip = "SW1A 1AA",
                        Country = CountryCode.GB
                    }
                },
                Shipping = new ShippingDetails
                {
                    Address = new Address
                    {
                        AddressLine1 = "123 High St.",
                        City = "London",
                        Zip = "SW1A 1AA",
                        Country = CountryCode.GB
                    }
                },
                Customer = new Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Customer.Customer
                {
                    Email = "john.doe@example.com",
                    Name = "John Doe"
                },
                Sender = new PaymentSender(PaymentSenderType.Individual),
                Instruction = new PaymentInstruction
                {
                    Purpose = PaymentPurposeType.FinancialServices
                },
                Metadata = new Dictionary<string, object>
                {
                    { "order_id", "ord_001" },
                    { "campaign", "spring_sale" }
                },
                PaymentMethodConfiguration = new PaymentMethodConfiguration
                {
                    Card = new CardConfiguration()
                }
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldNotBeNull();
            json.ShouldContain("session_data");
            json.ShouldContain("processing_channel_id");
            json.ShouldContain("capture_on");
            json.ShouldContain("metadata");
            json.ShouldContain("payment_method_configuration");
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new PaymentSessionSubmitRequest
            {
                SessionData = "token_abc123",
                Amount = 2500,
                Currency = Currency.USD,
                Reference = "ORD-456",
                ProcessingChannelId = "pc_test",
                Capture = false,
                Metadata = new Dictionary<string, object> { { "key", "value" } }
            };

            var serializer = new JsonSerializer();
            var json = serializer.Serialize(original);
            var deserialized = (PaymentSessionSubmitRequest)serializer.Deserialize(json, typeof(PaymentSessionSubmitRequest));

            deserialized.SessionData.ShouldBe("token_abc123");
            deserialized.Amount.ShouldBe(2500L);
            deserialized.Currency.ShouldBe(Currency.USD);
            deserialized.Reference.ShouldBe("ORD-456");
            deserialized.ProcessingChannelId.ShouldBe("pc_test");
            deserialized.Capture.ShouldBe(false);
            deserialized.Metadata["key"].ToString().ShouldBe("value");
        }

        [Fact]
        public void ShouldSerializeSnakeCaseKeys()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "tok",
                ProcessingChannelId = "pc_test",
                CaptureOn = new DateTime(2026, 1, 1),
                PaymentMethodConfiguration = new PaymentMethodConfiguration()
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"session_data\"");
            json.ShouldContain("\"processing_channel_id\"");
            json.ShouldContain("\"capture_on\"");
            json.ShouldContain("\"payment_method_configuration\"");
        }
    }
}
