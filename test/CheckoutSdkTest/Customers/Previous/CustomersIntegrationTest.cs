using Checkout.Common;
using Checkout.Instruments;
using Checkout.Instruments.Previous;
using Checkout.Tokens;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Customers.Previous
{
    public class CustomersIntegrationTest : SandboxTestFixture
    {
        public CustomersIntegrationTest() : base(PlatformType.Previous)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetCustomer()
        {
            var request = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone
                {
                    CountryCode = "1",
                    Number = "4155552671"
                }
            };
            var customerResponse = await PreviousApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerDetails = await PreviousApi.CustomersClient().Get(customerResponse.Id);
            customerDetails.ShouldNotBeNull();
            customerDetails.Email.ShouldBe(request.Email);
            customerDetails.Name.ShouldBe(request.Name);
            customerDetails.Phone.ShouldNotBeNull();
            customerDetails.DefaultId.ShouldBeNull();
            customerDetails.Instruments.ShouldBeEmpty();
        }

        [Fact]
        private async Task ShouldCreateAndUpdateCustomer()
        {
            //Create Customer
            var request = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone
                {
                    CountryCode = "1",
                    Number = "4155552671"
                }
            };
            var customerResponse = await PreviousApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();
            //Edit Customer
            request.Email = GenerateRandomEmail();
            request.Name = "Changed Name";

            var customerId = customerResponse.Id;
            await PreviousApi.CustomersClient().Update(customerId, request);

            var customerDetails = await PreviousApi.CustomersClient().Get(customerId);
            customerDetails.ShouldNotBeNull();
            customerDetails.Email.ShouldBe(request.Email);
            customerDetails.Name.ShouldBe(request.Name);
        }

        [Fact]
        private async Task ShouldCreateAndDeleteCustomer()
        {
            var request = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone
                {
                    CountryCode = "1",
                    Number = "4155552671"
                }
            };
            var customerResponse = await PreviousApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerId = customerResponse.Id;
            var emptyResponse = await PreviousApi.CustomersClient().Delete(customerId);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();
            await AssertNotFound(PreviousApi.CustomersClient().Get(customerId));
        }

        [Fact]
        private async Task ShouldGetCustomerDetailsWithInstrument()
        {
            var cardTokenResponse = await PreviousApi.TokensClient().Request(GetCardTokenRequest());
            cardTokenResponse.ShouldNotBeNull();

            var request = new CreateInstrumentRequest
            {
                Token = cardTokenResponse.Token,
                Customer = new InstrumentCustomerRequest
                {
                    Email = "instrumentcustomer@checkout.com",
                    Name = "Instrument Customer",
                    Default = true,
                    Phone = new Phone
                    {
                        CountryCode = "+1",
                        Number = "4155552671"
                    }
                }
            };

            var createInstrumentResponse = await PreviousApi.InstrumentsClient().Create(request);
            createInstrumentResponse.ShouldNotBeNull();
            createInstrumentResponse.Customer.ShouldNotBeNull();
            createInstrumentResponse.Customer.Id.ShouldNotBeNullOrEmpty();

            CustomerDetailsResponse customerDetailsResponse = await PreviousApi.CustomersClient().Get(createInstrumentResponse.Customer.Id);

            customerDetailsResponse.ShouldNotBeNull();
            customerDetailsResponse.Instruments.ShouldNotBeNull();
            customerDetailsResponse.Instruments.Count.ShouldBe(1);

            InstrumentDetails instrumentDetails = customerDetailsResponse.Instruments[0];
            createInstrumentResponse.Id.ShouldBe(instrumentDetails.Id);
            createInstrumentResponse.Type.ShouldBe(InstrumentType.Card);
            createInstrumentResponse.Fingerprint.ShouldBe(instrumentDetails.Fingerprint);
            createInstrumentResponse.ExpiryMonth.ShouldBe(instrumentDetails.ExpiryMonth);
            createInstrumentResponse.ExpiryYear.ShouldBe(instrumentDetails.ExpiryYear);
            createInstrumentResponse.Scheme.ShouldBe(instrumentDetails.Scheme);
            createInstrumentResponse.Last4.ShouldBe(instrumentDetails.Last4);
            createInstrumentResponse.Bin.ShouldBe(instrumentDetails.Bin);
            createInstrumentResponse.CardType.ShouldBe(CardType.Credit);
            createInstrumentResponse.Issuer.ShouldBe(instrumentDetails.Issuer);
            createInstrumentResponse.IssuerCountry.ShouldNotBeNull();
            createInstrumentResponse.ProductId.ShouldBe(instrumentDetails.ProductId);
            createInstrumentResponse.ProductType.ShouldBe(instrumentDetails.ProductType);
        }

        private CardTokenRequest GetCardTokenRequest()
        {
            var phone = new Phone
            {
                CountryCode = "44",
                Number = "020 222333"
            };

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var cardTokenRequest = new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            return cardTokenRequest;
        }
    }
}