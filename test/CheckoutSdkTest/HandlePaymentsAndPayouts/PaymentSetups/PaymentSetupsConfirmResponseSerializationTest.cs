using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Setups;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    public class PaymentSetupsConfirmResponseSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldDeserializeConfirmResponseWithSetupsEntitiesModels()
        {
            const string json = @"{
                ""id"": ""psu_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""processing_channel_id"": ""pc_q4dbxom5jbgudnjzjpz7j2z6uq"",
                ""amount"": 10000,
                ""currency"": ""GBP"",
                ""payment_type"": ""Regular"",
                ""reference"": ""REF-0987-475"",
                ""customer"": {
                    ""name"": ""John Smith"",
                    ""email"": { ""address"": ""johnsmith@example.com"", ""verified"": true }
                }
            }";

            var result =
                (PaymentSetupsConfirmResponse)_serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse));

            result.ShouldNotBeNull();
            result.Id.ShouldBe("psu_y3oqhf46pyzuxjbcn2giaqnb44");
            result.Amount.ShouldBe(10000L);
            result.Currency.ShouldBe(Currency.GBP);
            result.PaymentType.ShouldBe(PaymentType.Regular);

            // The customer must be the setups Customer, whose email is a nested object
            // (the payments CustomerResponse models email as a plain string and would not deserialize this shape).
            result.Customer.ShouldNotBeNull();
            result.Customer.Name.ShouldBe("John Smith");
            result.Customer.Email.ShouldNotBeNull();
            result.Customer.Email.Address.ShouldBe("johnsmith@example.com");
            result.Customer.Email.Verified.ShouldBe(true);
        }
    }
}
