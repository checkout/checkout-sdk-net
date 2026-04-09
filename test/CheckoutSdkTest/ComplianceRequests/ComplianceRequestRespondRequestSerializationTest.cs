using Checkout.ComplianceRequests.Requests;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.ComplianceRequests
{
    public class ComplianceRequestRespondRequestSerializationTest
    {
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
        public void ShouldRoundTripSerialize()
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
    }
}
