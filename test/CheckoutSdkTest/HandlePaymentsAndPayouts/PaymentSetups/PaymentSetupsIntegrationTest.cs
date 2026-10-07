using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Setups;
using Checkout.Payments.Setups.Entities;
using Checkout.Payments.Setups.Requests;
using Checkout.Payments.Setups.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    public class PaymentSetupsIntegrationTest : SandboxTestFixture
    {
        public PaymentSetupsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        public async Task CreatePaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();

            // Act
            var response = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.ProcessingChannelId.ShouldBe(paymentSetupsRequest.ProcessingChannelId);
            response.Amount.ShouldBe(paymentSetupsRequest.Amount);
            response.Currency.ShouldBe(paymentSetupsRequest.Currency);
            response.PaymentType.ShouldBe(paymentSetupsRequest.PaymentType);
            response.Reference.ShouldBe(paymentSetupsRequest.Reference);
            response.Description.ShouldBe(paymentSetupsRequest.Description);
        }

        [Fact]
        public async Task UpdatePaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();
            var createResponse = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            var updateRequest = CreateValidPaymentSetupsRequest();
            updateRequest.Description = "Updated description";

            // Act
            var response = await DefaultApi.PaymentSetupsClient().UpdatePaymentSetup(createResponse.Id, updateRequest);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(createResponse.Id);
            response.Description.ShouldBe("Updated description");
        }

        [Fact]
        public async Task GetPaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();
            var createResponse = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            // Act
            var response = await DefaultApi.PaymentSetupsClient().GetPaymentSetup(createResponse.Id);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(createResponse.Id);
            response.ProcessingChannelId.ShouldBe(paymentSetupsRequest.ProcessingChannelId);
            response.Amount.ShouldBe(paymentSetupsRequest.Amount);
            response.Currency.ShouldBe(paymentSetupsRequest.Currency);
            response.PaymentType.ShouldBe(paymentSetupsRequest.PaymentType);
            response.Reference.ShouldBe(paymentSetupsRequest.Reference);
            response.Description.ShouldBe(paymentSetupsRequest.Description);
        }

        [Fact(Skip = "Integration test - requires valid payment method option")]
        public async Task ConfirmPaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();
            var createResponse = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            // The name of the payment method to process the payment with (for example, tabby, klarna, card)
            var paymentMethodName = "card";

            // Act
            var response = await DefaultApi.PaymentSetupsClient().ConfirmPaymentSetup(createResponse.Id, paymentMethodName);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Amount.ShouldBe(paymentSetupsRequest.Amount);
            response.Currency.ShouldBe(paymentSetupsRequest.Currency);
        }

        [Fact]
        public async Task CreatePaymentSetupWithDeviceDetails_ShouldEchoDeviceAndReadEveryStatus()
        {
            // Arrange
            var request = CreateCashAppPaymentSetupsRequest();

            // Act
            var created = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(request);
            var fetched = await DefaultApi.PaymentSetupsClient().GetPaymentSetup(created.Id);

            // Assert
            var device = fetched.Customer.Device;
            device.Locale.ShouldBe("en_US");
            device.Fingerprint.ShouldBe("fp_abc123xyz");
            device.Ipv4.ShouldBe("203.0.113.0");
            device.Client.ShouldBe(CustomerDeviceClient.Web);
            device.Os.ShouldBe(CustomerDeviceOs.Ios);

            // A status value the SDK does not model reads as null and is then dropped on write, so
            // every payment method the API returned must still carry its status here.
            var methods = Newtonsoft.Json.Linq.JObject.Parse(new JsonSerializer().Serialize(fetched.PaymentMethods));
            methods.Count.ShouldBeGreaterThan(0);
            foreach (var method in methods.Properties())
            {
                ((Newtonsoft.Json.Linq.JObject)method.Value).ContainsKey("status")
                    .ShouldBeTrue(method.Name + " lost its status");
            }
        }

        [Fact]
        public async Task CreatePaymentSetupWithCustomerIdentifiers_ShouldEchoThem()
        {
            // Arrange
            var request = CreateValidPaymentSetupsRequest();
            request.Customer.Id = "cus_123456789";
            request.Customer.Country = CountryCode.GB;
            request.Customer.TaxNumber = "GB123456789";

            // Act
            var created = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(request);
            var fetched = await DefaultApi.PaymentSetupsClient().GetPaymentSetup(created.Id);

            // Assert
            fetched.Customer.Id.ShouldBe("cus_123456789");
            fetched.Customer.Country.ShouldBe(CountryCode.GB);
            fetched.Customer.TaxNumber.ShouldBe("GB123456789");
        }

        [Fact(Skip = "Requires a sandbox processing channel with Cash App Pay enabled")]
        public async Task CreatePaymentSetupWithCashApp_ShouldReturnCashAppDetails()
        {
            // Arrange
            var request = CreateCashAppPaymentSetupsRequest();

            // Act
            var created = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(request);
            var fetched = await DefaultApi.PaymentSetupsClient().GetPaymentSetup(created.Id);

            // Assert
            created.AvailablePaymentMethods.ShouldContain("cashapp");
            var cashApp = fetched.PaymentMethods.CashApp;
            cashApp.ShouldNotBeNull();
            cashApp.Status.ShouldNotBeNull();
            cashApp.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            cashApp.CustomerProfileSharing.ShouldBe(true);
        }

        private PaymentSetupsRequest CreateCashAppPaymentSetupsRequest()
        {
            var request = CreateValidPaymentSetupsRequest();
            request.Currency = Currency.USD;
            request.PaymentMethods = new Checkout.Payments.Setups.Entities.PaymentMethods
            {
                CashApp = new CashApp
                {
                    Initialization = PaymentMethodInitialization.Enabled,
                    CustomerProfileSharing = true
                }
            };
            request.Customer.Device = new CustomerDevice
            {
                Locale = "en_US",
                Fingerprint = "fp_abc123xyz",
                Ipv4 = "203.0.113.0",
                Client = CustomerDeviceClient.Web,
                Os = CustomerDeviceOs.Ios
            };
            return request;
        }

        private PaymentSetupsRequest CreateValidPaymentSetupsRequest()
        {
            return new PaymentSetupsRequest
            {
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                Amount = 1000,
                Currency = Currency.GBP,
                PaymentType = PaymentType.Regular,
                Reference = $"TEST-REF-{RandomString(6)}",
                Description = "Integration test payment setup",
                Settings = new Settings
                {
                    SuccessUrl = "https://example.com/success",
                    FailureUrl = "https://example.com/failure"
                },
                Customer = new Customer
                {
                    Name = "John Smith",
                    Email = new CustomerEmail
                    {
                        Address = $"john.smith+{RandomString(6)}@example.com",
                        Verified = true
                    },
                    Phone = new Phone
                    {
                        CountryCode = "+44",
                        Number = "207 946 0000"
                    },
                    Device = new CustomerDevice
                    {
                        Locale = "en_GB"
                    }
                },
                PaymentMethods = new Checkout.Payments.Setups.Entities.PaymentMethods
                {
                    // Configure basic payment methods for testing
                    Klarna = new Klarna
                    {
                        Initialization = PaymentMethodInitialization.Disabled,
                        AccountHolder = new KlarnaAccountHolder
                        {
                            Name = "John Klarna"
                        }
                    }
                }
            };
        }
    }
}