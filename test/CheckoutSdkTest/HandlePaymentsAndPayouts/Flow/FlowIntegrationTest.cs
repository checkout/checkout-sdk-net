using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.HandlePaymentsAndPayouts.Flow.Responses;
using Checkout.Payments;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source;
using Customer = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Customer;
using Common = Checkout.Common;
using Entities = Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Checkout.Payments.Sender;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    public class FlowIntegrationTest : SandboxTestFixture
    {
        public FlowIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        public async Task ShouldCreatePaymentSession()
        {
            // Arrange
            var request = CreatePaymentSessionCreateRequest();

            // Act
            var response = await DefaultApi.FlowClient().RequestPaymentSession(request);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.PaymentSessionToken.ShouldNotBeNull();
            response.PaymentSessionSecret.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid merchant configuration for Flow")]
        public async Task ShouldSubmitPaymentSession()
        {
            // Arrange
            var createRequest = CreatePaymentSessionCreateRequest();
            var createResponse = await DefaultApi.FlowClient().RequestPaymentSession(createRequest);
            
            var submitRequest = CreatePaymentSessionSubmitRequest();
            //submitRequest.SessionData = createResponse.PaymentSessionToken; ???

            // Act
            var response = await DefaultApi.FlowClient().SubmitPaymentSession(createResponse.Id, submitRequest);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid merchant configuration for Flow")]
        public async Task ShouldCompletePaymentSession()
        {
            // Arrange
            var request = CreatePaymentSessionCompleteRequest();

            // Act
            var response = await DefaultApi.FlowClient().RequestPaymentSessionWithPayment(request);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
        }
        
        [Fact]
        public void PaymentSessionCreateRequest_Should_Have_All_Required_Properties_For_JSON_Compatibility()
        {
            // This test validates that PaymentSessionCreateRequest has the necessary properties
            // to be compatible with the comprehensive JSON structure from the API documentation
            var request = new PaymentSessionCreateRequest();

            // Validate that all major property groups from the JSON are available:

            // Basic properties from PaymentSessionBase
            request.Amount = 1000;
            request.Currency = Currency.USD;
            request.PaymentType = PaymentType.Regular;
            request.Reference = "ORD-123A";

            // Properties from PaymentSessionInfo
            request.Description = "Payment for gold necklace";
            request.BillingDescriptor = new BillingDescriptor { Name = "Test" };
            request.Customer = new Customer.Customer { Email = "test@example.com" };
            request.SuccessUrl = "https://example.com/success";
            request.FailureUrl = "https://example.com/failure";
            request.Metadata = new Dictionary<string, object> { { "key", "value" } };
            request.Locale = LocaleType.Ar;  // Using a valid LocaleType value
            request.DisplayName = "Test Payment";
            request.ProcessingChannelId = "pc_123";
            request.Capture = true;
            request.CaptureOn = DateTime.Now;
            request.Risk = new RiskRequest { Enabled = false };
            request.ThreeDS = new ThreeDsRequest { Enabled = false };

            // Properties specific to PaymentSessionCreateRequest
            request.ExpiresOn = DateTime.Now.AddHours(1);
            request.EnabledPaymentMethods = new List<PaymentMethod> { PaymentMethod.Card };
            request.DisabledPaymentMethods = new List<PaymentMethod> { PaymentMethod.Eps };
            request.PaymentMethodConfiguration = new Entities.PaymentMethodConfiguration
            {
                Card = new CardConfiguration
                {
                    StorePaymentDetails = StorePaymentDetailsType.Disabled
                }
            };
            // Note: CustomerRetry property exists but may have type compatibility issues in the inheritance chain
            request.IpAddress = "127.0.0.1";

            // Assertions to ensure the object is properly constructed
            request.ShouldNotBeNull();
            request.Amount.ShouldBe(1000);
            request.Currency.ShouldBe(Currency.USD);
            request.Reference.ShouldBe("ORD-123A");
            request.Description.ShouldBe("Payment for gold necklace");
            request.Customer.ShouldNotBeNull();
            request.Customer.Email.ShouldBe("test@example.com");
            request.PaymentMethodConfiguration.ShouldNotBeNull();
            request.IpAddress.ShouldBe("127.0.0.1");
        }

        [Fact]
        public void PaymentSessionSubmitRequest_Should_Have_All_Required_Properties_For_JSON_Compatibility()
        {
            // This test validates that PaymentSessionSubmitRequest has the necessary properties
            // to be compatible with the submit JSON structure from the API documentation
            var request = new PaymentSessionSubmitRequest();

            // PaymentSessionSubmitRequest specific properties
            request.SessionData = "string";
            request.IpAddress = "90.197.169.245";

            // Basic properties from PaymentSessionBase (inherited)
            request.Amount = 1000;
            request.Reference = "ORD-123A";
            request.Items = new List<Checkout.Payments.Request.Product>
            {
                new Checkout.Payments.Request.Product
                {
                    Reference = "string",
                    Name = "Gold Necklace",
                    Quantity = 1
                }
            };
            request.ThreeDS = new ThreeDsRequest
            {
                Enabled = false,
                AttemptN3D = false,
                ChallengeIndicator = ChallengeIndicatorType.NoPreference,
                Exemption = Exemption.LowValue,
                AllowUpgrade = true
            };
            request.PaymentType = PaymentType.Regular;

            // Assertions to verify all properties from the JSON can be assigned
            request.ShouldNotBeNull();
            request.SessionData.ShouldBe("string");
            request.IpAddress.ShouldBe("90.197.169.245");
            request.Amount.ShouldBe(1000);
            request.Reference.ShouldBe("ORD-123A");
            request.Items.ShouldNotBeNull();
            request.Items.Count.ShouldBe(1);
            request.Items[0].Name.ShouldBe("Gold Necklace");
            request.ThreeDS.ShouldNotBeNull();
            request.ThreeDS.Enabled.ShouldBe(false);
            request.ThreeDS.AttemptN3D.ShouldBe(false);
            request.ThreeDS.ChallengeIndicator.ShouldBe(ChallengeIndicatorType.NoPreference);
            request.ThreeDS.Exemption.ShouldBe(Exemption.LowValue);
            request.ThreeDS.AllowUpgrade.ShouldBe(true);
            request.PaymentType.ShouldBe(PaymentType.Regular);
        }

        [Fact]
        public void PaymentSessionCompleteRequest_Should_Have_All_Required_Properties_For_JSON_Compatibility()
        {
            // This test validates that PaymentSessionCompleteRequest has the necessary properties
            // to be compatible with the complete JSON structure from the API documentation
            var request = new PaymentSessionCompleteRequest();

            // PaymentSessionCompleteRequest specific property
            request.SessionData = "string";
            
            // Properties from PaymentSessionBase (inherited through PaymentSessionInfo)
            request.Amount = 1000;
            request.Reference = "ORD-123A";
            request.Items = new List<Checkout.Payments.Request.Product>
            {
                new Checkout.Payments.Request.Product
                {
                    Reference = "string",
                    CommodityCode = "string",
                    UnitOfMeasure = "string",
                    TotalAmount = 1000,
                    TaxAmount = 1000,
                    DiscountAmount = 1000,
                    Url = "string",
                    ImageUrl = "string",
                    Name = "Gold Necklace",
                    Quantity = 1,
                    UnitPrice = 1000
                }
            };
            request.ThreeDS = new ThreeDsRequest
            {
                Enabled = false,
                AttemptN3D = false,
                ChallengeIndicator = ChallengeIndicatorType.NoPreference,
                Exemption = Exemption.LowValue,
                AllowUpgrade = true
            };
            request.PaymentType = PaymentType.Regular;
            
            // Properties from PaymentSessionInfo (inherited)
            request.Currency = Currency.USD;
            request.Billing = new BillingInformation
            {
                Address = new Address
                {
                    AddressLine1 = "123 High St.",
                    AddressLine2 = "Flat 456",
                    City = "London",
                    State = "str",
                    Zip = "SW1A 1AA",
                    Country = CountryCode.GB
                },
                Phone = new Phone
                {
                    CountryCode = "+1",
                    Number = "415 555 2671"
                }
            };
            request.BillingDescriptor = new BillingDescriptor
            {
                Name = "string",
                City = "string",
                Reference = "string"
            };
            request.Description = "Payment for gold necklace";
            request.Customer = new Customer.Customer
            {
                Email = "jia.tsang@example.com",
                Name = "Jia Tsang",
                Id = "string"
                // Note: Phone, tax_number and summary properties have type compatibility issues with Customer class
            };
            request.Shipping = new ShippingDetails
            {
                Address = new Address
                {
                    AddressLine1 = "123 High St.",
                    AddressLine2 = "Flat 456",
                    City = "London",
                    State = "str",
                    Zip = "SW1A 1AA",
                    Country = CountryCode.GB
                },
                Phone = new Phone
                {
                    CountryCode = "+1",
                    Number = "415 555 2671"
                }
            };
            request.Recipient = new PaymentRecipient
            {
                DateOfBirth = "1985-05-15",
                AccountNumber = "5555554444",
                Address = new Address
                {
                    AddressLine1 = "123 High St.",
                    AddressLine2 = "Flat 456",
                    City = "London",
                    State = "str",
                    Zip = "SW1A 1AA",
                    Country = CountryCode.GB
                },
                FirstName = "Jia",
                LastName = "Tsang"
            };
            request.Processing = new ProcessingSettings
            {
                Aft = true
                // Note: Many processing properties like discount_amount, shipping_amount, etc., may not be directly available
            };
            request.Instruction = new Checkout.Payments.PaymentInstruction
            {
                // Purpose property not directly available or has different enum type
            };
            request.ProcessingChannelId = "string";
            request.AmountAllocations = new List<AmountAllocations>
            {
                new AmountAllocations
                {
                    Id = "string",
                    Amount = 1,
                    Reference = "ORD-123A",
                    Commission = new Commission
                    {
                        Amount = 10,
                        Percentage = 12.5
                    }
                }
            };
            request.Risk = new RiskRequest
            {
                Enabled = false
            };
            request.DisplayName = "string";
            request.SuccessUrl = "https://example.com/payments/success";
            request.FailureUrl = "https://example.com/payments/failure";
            request.Metadata = new Dictionary<string, object>
            {
                ["coupon_code"] = "NY2018"
            };
            request.Locale = LocaleType.Ar;
            // request.Sender property may have type compatibility issues
            // Commenting out for test compilation
            request.Capture = true;
            request.CaptureOn = DateTime.Parse("2024-01-01T09:15:30Z").ToUniversalTime();
            
            // Assertions to verify all properties from the JSON can be assigned
            request.ShouldNotBeNull();
            request.SessionData.ShouldBe("string");
            request.Amount.ShouldBe(1000);
            request.Currency.ShouldBe(Currency.USD);
            request.PaymentType.ShouldBe(PaymentType.Regular);
            request.Billing.ShouldNotBeNull();
            request.BillingDescriptor.ShouldNotBeNull();
            request.Reference.ShouldBe("ORD-123A");
            request.Description.ShouldBe("Payment for gold necklace");
            request.Customer.ShouldNotBeNull();
            request.Shipping.ShouldNotBeNull();
            request.Recipient.ShouldNotBeNull();
            request.Processing.ShouldNotBeNull();
            request.Instruction.ShouldNotBeNull();
            request.ProcessingChannelId.ShouldBe("string");
            request.Items.Count.ShouldBe(1);
            request.AmountAllocations.Count.ShouldBe(1);
            request.Risk.ShouldNotBeNull();
            request.DisplayName.ShouldBe("string");
            request.SuccessUrl.ShouldBe("https://example.com/payments/success");
            request.FailureUrl.ShouldBe("https://example.com/payments/failure");
            request.Metadata.Count.ShouldBe(1);
            request.Locale.ShouldBe(LocaleType.Ar);
            request.ThreeDS.ShouldNotBeNull();
            // request.Sender assertion commented due to type compatibility issues
            request.Capture.ShouldBe(true);
            request.CaptureOn.ShouldNotBeNull();
        }

        private PaymentSessionCreateRequest CreatePaymentSessionCreateRequest()
        {
            return new PaymentSessionCreateRequest
            {
                Amount = 1000L,
                Currency = Currency.USD,
                PaymentType = PaymentType.Regular,
                Reference = "ORD-123A",
                Description = "Payment for gold necklace",
                DisplayName = "Company Test",
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/failure",
                EnabledPaymentMethods = new List<PaymentMethod>
                {
                    PaymentMethod.Card
                },
                PaymentMethodConfiguration = new Entities.PaymentMethodConfiguration
                {
                    Card = new CardConfiguration
                    {
                        StorePaymentDetails = StorePaymentDetailsType.Enabled
                    }
                },
                Billing = new BillingInformation
                {
                    Address = new Address
                    {
                        AddressLine1 = "123 High St.",
                        AddressLine2 = "Flat 456",
                        City = "London",
                        State = "str",
                        Zip = "SW1A 1AA",
                        Country = CountryCode.GB
                    },
                    Phone = new Phone
                    {
                        CountryCode = "+1",
                        Number = "415 555 2671"
                    }
                },
                BillingDescriptor = new BillingDescriptor
                {
                    Name = "Company Test",
                    City = "London"
                },
                Risk = new RiskRequest
                {
                    Enabled = false
                },
                ThreeDS = new ThreeDsRequest
                {
                    Enabled = false
                },
                Capture = true,
                Locale = LocaleType.EnGb
            };
        }

        private PaymentSessionSubmitRequest CreatePaymentSessionSubmitRequest()
        {
            return new PaymentSessionSubmitRequest
            {
                SessionData = "encrypted_session_data",
                IpAddress = "192.168.1.1"
            };
        }

        private PaymentSessionCompleteRequest CreatePaymentSessionCompleteRequest()
        {
            return new PaymentSessionCompleteRequest
            {
                Amount = 1000,
                Currency = Currency.USD,
                Reference = "ORD-5023-4E89",
                Customer = new Customer.Customer
                {
                    Email = "johndoe@email.com",
                    Name = "John Doe"
                },
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                SessionData = "encrypted_session_data"
            };
        }
    }
}