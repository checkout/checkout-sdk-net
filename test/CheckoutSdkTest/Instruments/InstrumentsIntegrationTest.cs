using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using Checkout.Payments;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using CustomerRequest = Checkout.Customers.CustomerRequest;

namespace Checkout.Instruments
{
    public class InstrumentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldCreateAndGetInstrument()
        {
            var tokenInstrument = await CreateTokenInstrument();
            tokenInstrument.ShouldNotBeNull();

            var getResponse = await DefaultApi.InstrumentsClient().Get(tokenInstrument.Id);
            getResponse.ShouldNotBeNull();

            var cardResponse = (GetCardInstrumentResponse)getResponse;
            cardResponse.ShouldNotBeNull();
            cardResponse.Id.ShouldNotBeNull();
            cardResponse.Fingerprint.ShouldNotBeNull();
            cardResponse.ExpiryMonth.ShouldNotBeNull();
            cardResponse.ExpiryYear.ShouldNotBeNull();
            cardResponse.Scheme.ShouldNotBeNull();
            cardResponse.Last4.ShouldNotBeNull();
            cardResponse.Bin.ShouldNotBeNull();
            cardResponse.IssuerCountry.ShouldNotBeNull();
            cardResponse.ProductId.ShouldNotBeNull();
            cardResponse.ProductType.ShouldNotBeNull();
            cardResponse.Customer.ShouldNotBeNull();
            cardResponse.AccountHolder.ShouldNotBeNull();
            cardResponse.AccountHolder.BillingAddress.ShouldNotBeNull();
            cardResponse.AccountHolder.Phone.ShouldNotBeNull();
            cardResponse.CardType.ShouldNotBeNull();
            cardResponse.CardCategory.ShouldNotBeNull();
        }
        
        [Fact]
        private async Task ShouldCreateInstrumentSepa()
        {
            var request = new CreateSepaInstrumentRequest
            {
                Type = InstrumentType.Sepa,
                InstrumentData = new InstrumentData
                {
                    AccountNumber = "FR7630006000011234567890189",
                    Country = CountryCode.FR,
                    Currency = Currency.EUR,
                    PaymentType = PaymentType.Recurring,
                },
                AccountHolder = new AccountHolder
                {
                    Type = AccountHolderType.Individual,
                    FirstName = "Ali",
                    LastName = "Farid",
                    DateOfBirth = "1986-01-01T00:00:00.000Z",
                    BillingAddress = new Address
                    {
                        AddressLine1 = "Rue Exemple",
                        AddressLine2 = "1",
                        City = "Paris",
                        Zip = "1234",
                        Country = CountryCode.FR
                    },
                    Phone = new Phone
                    {
                        CountryCode = "33",
                        Number = "123456789"
                    }
                }
            };
            
            var response = await DefaultApi.InstrumentsClient().Create(request);
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Fingerprint.ShouldNotBeNull();
        }

        [Fact(Skip = "Unavailable")]
        private async Task ShouldUpdateTokenInstrument()
        {
            var tokenInstrument = await CreateTokenInstrument();
            tokenInstrument.ShouldNotBeNull();

            var tokenResponse = await RequestToken();
            tokenResponse.Token.ShouldNotBeNull();

            var updateInstrumentTokenRequest = new UpdateTokenInstrumentRequest {Token = tokenResponse.Token};

            var updateResponse =
                await DefaultApi.InstrumentsClient().Update(tokenInstrument.Id, updateInstrumentTokenRequest);

            updateResponse.ShouldNotBeNull();
            updateResponse.HttpStatusCode.ShouldNotBeNull();
            updateResponse.ResponseHeaders.ShouldNotBeNull();
            updateResponse.Body.ShouldNotBeNull();

            var updateCardInstrumentResponse = (UpdateCardInstrumentResponse)updateResponse;
            updateCardInstrumentResponse.Fingerprint.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdateCardInstrument()
        {
            var tokenInstrument = await CreateTokenInstrument();
            tokenInstrument.ShouldNotBeNull();

            var updateCardInstrument = new UpdateCardInstrumentRequest
            {
                ExpiryMonth = 12,
                ExpiryYear = 2030,
                Name = "John Doe",
                Customer = new UpdateCustomerRequest {Id = tokenInstrument.Customer.Id, Default = true},
                AccountHolder = new AccountHolder
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = new Phone {CountryCode = "+1", Number = "415 555 2671"},
                    BillingAddress = new Address
                    {
                        AddressLine1 = "CheckoutSdk.com",
                        AddressLine2 = "90 Tottenham Court Road",
                        City = "London",
                        State = "London",
                        Zip = "W1T 4TJ",
                        Country = CountryCode.GB
                    }
                }
            };

            var updateInstrumentResponse =
                await DefaultApi.InstrumentsClient().Update(tokenInstrument.Id, updateCardInstrument);
            updateInstrumentResponse.ShouldNotBeNull();
            var updateCardInstrumentResponse = (UpdateCardInstrumentResponse)updateInstrumentResponse;
            updateCardInstrumentResponse.Fingerprint.ShouldNotBeNullOrEmpty();

            var getResponse = await DefaultApi.InstrumentsClient().Get(tokenInstrument.Id);
            getResponse.ShouldNotBeNull();

            var cardResponse = (GetCardInstrumentResponse)getResponse;
            cardResponse.ShouldNotBeNull();
            cardResponse.Id.ShouldNotBeNull();
            cardResponse.Fingerprint.ShouldNotBeNull();
            cardResponse.ExpiryMonth.ShouldBe(12);
            cardResponse.ExpiryYear.ShouldBe(2030);

            cardResponse.AccountHolder.FirstName.ShouldBe("John");
            cardResponse.AccountHolder.LastName.ShouldBe("Doe");
            cardResponse.CardType.ShouldNotBeNull();
            cardResponse.CardCategory.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldDeleteInstrument()
        {
            var tokenInstrument = await CreateTokenInstrument();
            tokenInstrument.ShouldNotBeNull();

            var emptyResponse = await DefaultApi.InstrumentsClient().Delete(tokenInstrument.Id);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            await AssertNotFound(DefaultApi.InstrumentsClient().Get(tokenInstrument.Id));
        }

        private async Task<CreateTokenInstrumentResponse> CreateTokenInstrument()
        {
            var phone = new Phone {CountryCode = "1", Number = "4155552671"};

            var customerRequest = new CustomerRequest {Email = GenerateRandomEmail(), Name = "Testing", Phone = phone};

            var customer = await DefaultApi.CustomersClient().Create(customerRequest);
            customer.ShouldNotBeNull();

            return await CreateTokenInstrument(customer.Id);
        }

        private async Task<CreateTokenInstrumentResponse> CreateTokenInstrument(string customerId)
        {
            var tokenResponse = await RequestToken();
            tokenResponse.Token.ShouldNotBeNull();

            var phone = new Phone {CountryCode = "+1", Number = "415 555 2671"};

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var accountHolder = new AccountHolder
            {
                FirstName = "John", LastName = "Smith", Phone = phone, BillingAddress = billingAddress
            };

            var customer = new CreateCustomerInstrumentRequest {Id = customerId};

            var createTokenInstrumentRequest = new CreateTokenInstrumentRequest
            {
                Token = tokenResponse.Token, AccountHolder = accountHolder, Customer = customer
            };

            var response = await DefaultApi.InstrumentsClient()
                .Create(createTokenInstrumentRequest);
            response.ShouldBeAssignableTo(typeof(CreateTokenInstrumentResponse));
            CreateTokenInstrumentResponse createTokenInstrumentResponse = (CreateTokenInstrumentResponse)response;
            createTokenInstrumentResponse.ShouldNotBeNull();
            createTokenInstrumentResponse.Id.ShouldNotBeNull();
            createTokenInstrumentResponse.Fingerprint.ShouldNotBeNull();
            createTokenInstrumentResponse.ExpiryMonth.ShouldNotBeNull();
            createTokenInstrumentResponse.ExpiryYear.ShouldNotBeNull();
            createTokenInstrumentResponse.Scheme.ShouldNotBeNull();
            createTokenInstrumentResponse.Last4.ShouldNotBeNull();
            createTokenInstrumentResponse.Bin.ShouldNotBeNull();
            createTokenInstrumentResponse.IssuerCountry.ShouldNotBeNull();
            createTokenInstrumentResponse.ProductId.ShouldNotBeNull();
            createTokenInstrumentResponse.ProductType.ShouldNotBeNull();
            createTokenInstrumentResponse.Customer.ShouldNotBeNull();
            createTokenInstrumentResponse.CardType.ShouldNotBeNull();
            createTokenInstrumentResponse.CardCategory.ShouldNotBeNull();
            return createTokenInstrumentResponse;
        }
    }
}