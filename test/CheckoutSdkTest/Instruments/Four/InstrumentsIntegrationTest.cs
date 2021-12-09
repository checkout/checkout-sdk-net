using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Common.Four;
using Checkout.Instruments.Four.Create;
using Checkout.Instruments.Four.Get;
using Checkout.Instruments.Four.Update;
using Checkout.Payments.Four;
using Shouldly;
using Xunit;
using CustomerRequest = Checkout.Customers.Four.CustomerRequest;

namespace Checkout.Instruments.Four
{
    public class InstrumentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldCreateAndGetInstrument()
        {
            var tokenInstrument = await CreateTokenInstrument();
            tokenInstrument.ShouldNotBeNull();

            var getResponse = await FourApi.InstrumentsClient().Get(tokenInstrument.Id);
            getResponse.ShouldNotBeNull();

            var cardResponse = (GetCardInstrumentResponse) getResponse;
            cardResponse.ShouldNotBeNull();
            cardResponse.Id.ShouldNotBeNull();
            cardResponse.Fingerprint.ShouldNotBeNull();
            cardResponse.ExpiryMonth.ShouldNotBeNull();
            cardResponse.ExpiryYear.ShouldNotBeNull();
            cardResponse.Scheme.ShouldNotBeNull();
            cardResponse.Last4.ShouldNotBeNull();
            cardResponse.Bin.ShouldNotBeNull();
            cardResponse.Issuer.ShouldNotBeNull();
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
        private async Task ShouldUpdateTokenInstrument()
        {
            var tokenInstrument = await CreateTokenInstrument();
            tokenInstrument.ShouldNotBeNull();

            var tokenResponse = await RequestToken();
            tokenResponse.Token.ShouldNotBeNull();

            var updateInstrumentTokenRequest = new UpdateTokenInstrumentRequest
            {
                Token = tokenResponse.Token
            };

            var updateResponse =
                await FourApi.InstrumentsClient().Update(tokenInstrument.Id, updateInstrumentTokenRequest);

            updateResponse.ShouldNotBeNull();

            var updateCardInstrumentResponse = (UpdateCardInstrumentResponse) updateResponse;
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
                ExpiryYear = 2024,
                Name = "John Doe",
                Customer = new UpdateCustomerRequest
                {
                    Id = tokenInstrument.Customer.Id,
                    Default = true
                },
                AccountHolder = new AccountHolder
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = new Phone
                    {
                        CountryCode = "+1",
                        Number = "415 555 2671"
                    },
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
                await FourApi.InstrumentsClient().Update(tokenInstrument.Id, updateCardInstrument);
            updateInstrumentResponse.ShouldNotBeNull();
            var updateCardInstrumentResponse = (UpdateCardInstrumentResponse) updateInstrumentResponse;
            updateCardInstrumentResponse.Fingerprint.ShouldNotBeNullOrEmpty();

            var getResponse = await FourApi.InstrumentsClient().Get(tokenInstrument.Id);
            getResponse.ShouldNotBeNull();

            var cardResponse = (GetCardInstrumentResponse) getResponse;
            cardResponse.ShouldNotBeNull();
            cardResponse.Id.ShouldNotBeNull();
            cardResponse.Fingerprint.ShouldNotBeNull();
            cardResponse.ExpiryMonth.ShouldBe(12);
            cardResponse.ExpiryYear.ShouldBe(2024);
            cardResponse.Customer.Default.ShouldBeTrue();
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

            await FourApi.InstrumentsClient().Delete(tokenInstrument.Id);

            await AssertNotFound(FourApi.InstrumentsClient().Get(tokenInstrument.Id));
        }

        private async Task<CreateTokenInstrumentResponse> CreateTokenInstrument()
        {
            var phone = new Phone
            {
                CountryCode = "1",
                Number = "4155552671"
            };

            var customerRequest = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Testing",
                Phone = phone
            };

            var customer = await FourApi.CustomersClient().Create(customerRequest);
            customer.ShouldNotBeNull();

            return await CreateTokenInstrument(customer.Id);
        }

        private async Task<CreateTokenInstrumentResponse> CreateTokenInstrument(string customerId)
        {
            var tokenResponse = await RequestToken();
            tokenResponse.Token.ShouldNotBeNull();

            var phone = new Phone
            {
                CountryCode = "+1",
                Number = "415 555 2671"
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

            var accountHolder = new AccountHolder
            {
                FirstName = "John",
                LastName = "Smith",
                Phone = phone,
                BillingAddress = billingAddress
            };

            var customer = new CreateCustomerInstrumentRequest
            {
                Id = customerId
            };

            var createTokenInstrumentRequest = new CreateTokenInstrumentRequest
            {
                Token = tokenResponse.Token,
                AccountHolder = accountHolder,
                Customer = customer
            };

            var response = await FourApi.InstrumentsClient()
                .Create<CreateTokenInstrumentResponse>(createTokenInstrumentRequest);
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Fingerprint.ShouldNotBeNull();
            response.ExpiryMonth.ShouldNotBeNull();
            response.ExpiryYear.ShouldNotBeNull();
            response.Scheme.ShouldNotBeNull();
            response.Last4.ShouldNotBeNull();
            response.Bin.ShouldNotBeNull();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.Customer.ShouldNotBeNull();
            response.CardType.ShouldNotBeNull();
            response.CardCategory.ShouldNotBeNull();
            return response;
        }
    }
}