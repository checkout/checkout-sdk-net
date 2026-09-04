using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.HandlePaymentsAndPayouts.Flow.Responses;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Checkout.Payments;
using PaymentInstruction = Checkout.Payments.PaymentInstruction;
using PaymentMethodConfiguration = Checkout.HandlePaymentsAndPayouts.Flow.Entities.PaymentMethodConfiguration;
using Shouldly;
using System.Collections.Generic;
using System;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    /// <summary>
    /// Schema validation tests for Checkout.HandlePaymentsAndPayouts.Flow.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class FlowSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // PaymentSessionCreateRequest
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithPaymentPlanAndAuthorizationType()
        {
            var request = new PaymentSessionCreateRequest
            {
                AuthorizationType = AuthorizationType.Estimated,
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Variable,
                    Amount = 1234L,
                    DaysBetweenPayments = 28,
                    TotalNumberOfPayments = 5,
                    Name = "Subscription 1234"
                }
            };

            Should.NotThrow(() => Serializer.Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeAuthorizationTypeAndPaymentPlan()
        {
            var original = new PaymentSessionCreateRequest
            {
                AuthorizationType = AuthorizationType.Estimated,
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Variable,
                    Amount = 1234L,
                    Name = "Subscription 1234"
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentSessionCreateRequest)Serializer.Deserialize(json, typeof(PaymentSessionCreateRequest));

            json.ShouldContain("\"authorization_type\":\"Estimated\"");
            json.ShouldContain("\"payment_plan\":");
            deserialized.AuthorizationType.ShouldBe(AuthorizationType.Estimated);
            deserialized.PaymentPlan.ShouldNotBeNull();
            deserialized.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Variable);
            deserialized.PaymentPlan.Amount.ShouldBe(1234L);
            deserialized.PaymentPlan.Name.ShouldBe("Subscription 1234");
        }

        [Fact]
        public void ShouldDeserializeAuthorizationTypeAndPaymentPlan()
        {
            const string json = @"{
                ""authorization_type"": ""Final"",
                ""payment_plan"": {
                    ""amount_variability"": ""Fixed"",
                    ""amount"": 500,
                    ""total_number_of_payments"": 6
                }
            }";

            var result = (PaymentSessionCreateRequest)Serializer.Deserialize(json, typeof(PaymentSessionCreateRequest));

            result.ShouldNotBeNull();
            result.AuthorizationType.ShouldBe(AuthorizationType.Final);
            result.PaymentPlan.ShouldNotBeNull();
            result.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Fixed);
            result.PaymentPlan.Amount.ShouldBe(500L);
            result.PaymentPlan.TotalNumberOfPayments.ShouldBe(6);
        }

        // ------------------------------------------------------------------------
        // PaymentSessionSubmitRequest
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForPaymentSessionSubmitRequest()
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
        public void ShouldRoundTripSerializeForPaymentSessionSubmitRequest()
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

        // ------------------------------------------------------------------------
        // PaymentSubmissionResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForPaymentSubmissionResponse()
        {
            var response = new PaymentSubmissionResponse();

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var response = new PaymentSubmissionResponse
            {
                Id = "pay_abc123",
                Status = "Approved",
                Type = PaymentMethod.Card,
                DeclineReason = "Insufficient funds",
                Action = new { type = "redirect", url = "https://example.com" },
                PaymentSessionId = "ps_xyz",
                PaymentSessionSecret = "secret_abc"
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeApprovedResponse()
        {
            const string json = @"{
                ""id"": ""pay_abc123"",
                ""status"": ""Approved"",
                ""type"": ""card"",
                ""payment_session_id"": ""ps_xyz"",
                ""payment_session_secret"": ""secret_abc""
            }";

            var response = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_abc123");
            response.Status.ShouldBe("Approved");
            response.PaymentSessionId.ShouldBe("ps_xyz");
            response.PaymentSessionSecret.ShouldBe("secret_abc");
        }

        [Fact]
        public void ShouldDeserializeDeclinedResponse()
        {
            const string json = @"{
                ""id"": ""pay_def456"",
                ""status"": ""Declined"",
                ""type"": ""card"",
                ""decline_reason"": ""Insufficient funds""
            }";

            var response = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_def456");
            response.Status.ShouldBe("Declined");
            response.DeclineReason.ShouldBe("Insufficient funds");
        }

        [Fact]
        public void ShouldDeserializeActionRequiredResponse()
        {
            const string json = @"{
                ""id"": ""pay_ghi789"",
                ""status"": ""Action Required"",
                ""type"": ""card"",
                ""action"": { ""type"": ""redirect"" }
            }";

            var response = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_ghi789");
            response.Status.ShouldBe("Action Required");
            response.Action.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerializeForPaymentSubmissionResponse()
        {
            var original = new PaymentSubmissionResponse
            {
                Id = "pay_abc123",
                Status = "Approved",
                Type = PaymentMethod.Card,
                PaymentSessionId = "ps_xyz",
                PaymentSessionSecret = "secret_abc",
                DeclineReason = null,
                Action = null
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            deserialized.Id.ShouldBe(original.Id);
            deserialized.Status.ShouldBe(original.Status);
            deserialized.Type.ShouldBe(original.Type);
            deserialized.PaymentSessionId.ShouldBe(original.PaymentSessionId);
            deserialized.PaymentSessionSecret.ShouldBe(original.PaymentSessionSecret);
        }
    }
}
