using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Source;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.
    CardDestination;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.IndividualAccountHolder;
using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.AccountHolder.Common.BillingAddress;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Instruction;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments
{
    public class HandlePaymentsAndPayoutsIntegrationTest : SandboxTestFixture
    {
        public HandlePaymentsAndPayoutsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "use on demand")]
        public async Task ShouldRequestPaymentWithUnreferencedRefund_CardDestination()
        {
            // Arrange
            var unreferencedRefundRequest = CreateUnreferencedRefundRequest();

            // Act
            var response = await DefaultApi.PaymentsClient().RequestPayment(unreferencedRefundRequest);

            // Assert
            response.ShouldNotBeNull();

            if (response.Created != null)
            {
                response.Created.Id.ShouldNotBeNull();
                response.Created.Status.ShouldNotBeNull();
                response.Created.Amount.ShouldBe(1000);
                response.Created.Currency.ShouldBe("USD");
                response.Created.Reference.ShouldBe("ORD-5023-4E89");
                response.Created.Source.ShouldNotBeNull();
                response.Created.Source.Type.ShouldNotBeNull();
            }
            else if (response.Accepted != null)
            {
                response.Accepted.Id.ShouldNotBeNull();
                response.Accepted.Status.ShouldNotBeNull();
                response.Accepted.Reference.ShouldBe("ORD-5023-4E89");
            }
            else
            {
                throw new Exception("Neither Created nor Accepted response was returned");
            }
        }

        [Fact(Skip = "use on demand")]
        public async Task ShouldRequestPaymentWithUnreferencedRefund_WithIdempotencyKey()
        {
            // Arrange
            var unreferencedRefundRequest = CreateUnreferencedRefundRequest();
            var idempotencyKey = Guid.NewGuid().ToString();

            // Act
            var response1 = await DefaultApi.PaymentsClient().RequestPayment(unreferencedRefundRequest, idempotencyKey);
            var response2 = await DefaultApi.PaymentsClient().RequestPayment(unreferencedRefundRequest, idempotencyKey);

            // Assert
            response1.ShouldNotBeNull();
            response2.ShouldNotBeNull();

            // With the same idempotency key, should get the same response
            if (response1.Created != null && response2.Created != null)
            {
                response1.Created.Id.ShouldBe(response2.Created.Id);
            }
            else if (response1.Accepted != null && response2.Accepted != null)
            {
                response1.Accepted.Id.ShouldBe(response2.Accepted.Id);
            }
        }

        private UnreferencedRefundRequest CreateUnreferencedRefundRequest()
        {
            return new UnreferencedRefundRequest
            {
                Amount = 1000,
                Currency = Currency.USD,
                PaymentType = "UnreferencedRefund",
                Reference = "REF-FQSGRB",
                Source = new Source { Type = "currency_account", Id = "ca_7hdklm89ybnfqqmk8pxrcvfh5t" },
                Destination = new CardDestination
                {
                    Number = "4111111111111111",
                    ExpiryMonth = 12,
                    ExpiryYear = 2042,
                    AccountHolder = new IndividualAccountHolder
                    {
                        FirstName = "John", 
                        LastName = "Doe",
                        DateOfBirth = "2000-01-01",
                        CountryOfBirth = CountryCode.US,
                        BillingAddress = new BillingAddress
                        {
                            AddressLine1 = "123 Test St",
                            City = "New York",
                            State = "NY",
                            Zip = "10014",
                            Country = CountryCode.US
                        }
                    }
                },
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                Instruction = new Instruction
                {
                    FundsTransferType = "C60",
                    Purpose = PurposeType.Income
                }
            };
        }
    }
}