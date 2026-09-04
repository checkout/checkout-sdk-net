using Checkout.ComplianceRequests.Requests;
using Checkout.ComplianceRequests.Responses;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.ComplianceRequests
{
    /// <summary>
    /// Schema validation tests for Checkout.ComplianceRequests.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class ComplianceRequestsSerializationTest
    {
        // ------------------------------------------------------------------------
        // ComplianceRequestRespondRequest
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredFields()
        {
            var request = new ComplianceRequestRespondRequest
            {
                Fields = new ComplianceRespondedFields
                {
                    Sender = new List<ComplianceRespondedField>
                    {
                        new ComplianceRespondedField { Name = "date_of_birth", Value = "2000-01-01", NotAvailable = false }
                    }
                }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var request = new ComplianceRequestRespondRequest
            {
                Fields = new ComplianceRespondedFields
                {
                    Sender = new List<ComplianceRespondedField>
                    {
                        new ComplianceRespondedField { Name = "date_of_birth", Value = "2000-01-01", NotAvailable = false }
                    },
                    Recipient = new List<ComplianceRespondedField>
                    {
                        new ComplianceRespondedField { Name = "full_name", Value = "John Doe", NotAvailable = false },
                        new ComplianceRespondedField { Name = "address", NotAvailable = true }
                    }
                },
                Comments = "Providing the requested information"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeForComplianceRequestRespondRequest()
        {
            var original = new ComplianceRequestRespondRequest
            {
                Fields = new ComplianceRespondedFields
                {
                    Sender = new List<ComplianceRespondedField>
                    {
                        new ComplianceRespondedField { Name = "date_of_birth", Value = "2000-01-01", NotAvailable = false }
                    }
                },
                Comments = "Test comment"
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (ComplianceRequestRespondRequest)serializer.Deserialize(json, typeof(ComplianceRequestRespondRequest));

            deserialized.Comments.ShouldBe(original.Comments);
            deserialized.Fields.ShouldNotBeNull();
            deserialized.Fields.Sender.ShouldNotBeNull();
            deserialized.Fields.Sender.Count.ShouldBe(1);
            deserialized.Fields.Sender[0].Name.ShouldBe("date_of_birth");
        }

        // ------------------------------------------------------------------------
        // ComplianceRequestDetailsResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var response = new ComplianceRequestDetailsResponse
            {
                PaymentId = "pay_fun26akvvjjerahhctaq2uzhu4",
                ClientId = "cli_vkuhvk4vjn2edkps7dfsq6emqm",
                EntityId = "ent_azsiyswl7bwe2ynjzujy7lcjca",
                SegmentId = "seg_siopnqocrc4ehgyer4tpepq5pp",
                Amount = "38.23",
                Currency = "HKD",
                RecipientName = "Jia Tsang",
                RequestedOn = "2026-02-04T15:46:44.9663151Z",
                Status = "pending",
                TransactionReference = "ref-abc",
                SenderReference = "ref-def",
                LastUpdated = "2026-02-04T15:47:18.6326970Z",
                SenderName = "Ali Farid",
                PaymentType = "PayToCard",
                Fields = new ComplianceRequestedFields
                {
                    Sender = new List<ComplianceRequestedField>
                    {
                        new ComplianceRequestedField { Name = "date_of_birth", Type = "date", Value = "2000-01-01" }
                    },
                    Recipient = new List<ComplianceRequestedField>
                    {
                        new ComplianceRequestedField { Name = "full_name", Type = "string" }
                    }
                }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""payment_id"": ""pay_fun26akvvjjerahhctaq2uzhu4"",
                ""client_id"": ""cli_vkuhvk4vjn2edkps7dfsq6emqm"",
                ""entity_id"": ""ent_azsiyswl7bwe2ynjzujy7lcjca"",
                ""amount"": ""38.23"",
                ""currency"": ""HKD"",
                ""recipient_name"": ""Jia Tsang"",
                ""status"": ""responded"",
                ""payment_type"": ""PayToCard"",
                ""fields"": {
                    ""sender"": [{ ""name"": ""date_of_birth"", ""type"": ""date"" }],
                    ""recipient"": []
                }
            }";

            var response = (ComplianceRequestDetailsResponse)new JsonSerializer()
                .Deserialize(json, typeof(ComplianceRequestDetailsResponse));

            response.ShouldNotBeNull();
            response.PaymentId.ShouldBe("pay_fun26akvvjjerahhctaq2uzhu4");
            response.Currency.ShouldBe("HKD");
            response.Status.ShouldBe("responded");
            response.Fields.ShouldNotBeNull();
            response.Fields.Sender.ShouldNotBeNull();
            response.Fields.Sender.Count.ShouldBe(1);
        }

        [Fact]
        public void ShouldRoundTripSerializeForComplianceRequestDetailsResponse()
        {
            var original = new ComplianceRequestDetailsResponse
            {
                PaymentId = "pay_test123",
                Status = "pending",
                Currency = "USD",
                Amount = "100.00"
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (ComplianceRequestDetailsResponse)serializer.Deserialize(json, typeof(ComplianceRequestDetailsResponse));

            deserialized.PaymentId.ShouldBe(original.PaymentId);
            deserialized.Status.ShouldBe(original.Status);
            deserialized.Currency.ShouldBe(original.Currency);
        }
    }
}
