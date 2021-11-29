using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Tokens;
using Shouldly;
using Xunit;

namespace Checkout.Instruments
{
    public class InstrumentsIntegrationTest : SandboxTestFixture
    {
        public InstrumentsIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetInstrument()
        {
            var createInstrumentResponse = await CreateTokenInstrument();

            createInstrumentResponse.ShouldNotBeNull();
            createInstrumentResponse.Bin.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.CardCategory.ShouldBe(CardCategory.Consumer);
            createInstrumentResponse.CardType.ShouldBe(CardType.Credit);
            createInstrumentResponse.Customer.ShouldNotBeNull();
            createInstrumentResponse.Customer.Default.ShouldBeFalse();
            createInstrumentResponse.Customer.Email.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.Customer.Id.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.Customer.Name.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.ExpiryMonth.ShouldBe(6);
            createInstrumentResponse.ExpiryYear.ShouldBe(2025);
            createInstrumentResponse.Fingerprint.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.Id.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.Issuer.ShouldNotBeNull();
            createInstrumentResponse.IssuerCountry.ShouldNotBeNull();
            createInstrumentResponse.Last4.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.Name.ShouldBeNull();
            createInstrumentResponse.ProductId.ShouldNotBeNull();
            createInstrumentResponse.ProductType.ShouldNotBeNullOrEmpty();
            createInstrumentResponse.Type.ShouldBe(InstrumentType.Card);

            var retrieveInstrumentResponse = await DefaultApi.InstrumentsClient().Get(createInstrumentResponse.Id);

            retrieveInstrumentResponse.ShouldNotBeNull();
            retrieveInstrumentResponse.Bin.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.CardCategory.ShouldBe(CardCategory.Consumer);
            retrieveInstrumentResponse.CardType.ShouldBe(CardType.Credit);
            retrieveInstrumentResponse.Customer.ShouldNotBeNull();
            retrieveInstrumentResponse.Customer.Default.ShouldBeTrue();
            retrieveInstrumentResponse.Customer.Email.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.Customer.Id.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.Customer.Name.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.ExpiryMonth.ShouldBe(6);
            retrieveInstrumentResponse.ExpiryYear.ShouldBe(2025);
            retrieveInstrumentResponse.Fingerprint.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.Id.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.Issuer.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.IssuerCountry.ShouldBe(CountryCode.US);
            retrieveInstrumentResponse.Last4.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.Name.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.ProductId.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.ProductType.ShouldNotBeNullOrEmpty();
            retrieveInstrumentResponse.Type.ShouldBe(InstrumentType.Card);
        }

        [Fact]
        private async Task ShouldCreateAndUpdateInstrument()
        {
            var createInstrumentResponse = await CreateTokenInstrument();

            var updateInstrumentRequest = new UpdateInstrumentRequest
            {
                Name = "New Name",
                ExpiryMonth = 12,
                ExpiryYear = 2026,
            };

            await DefaultApi.InstrumentsClient().Update(createInstrumentResponse.Id, updateInstrumentRequest);

            var retrieveInstrumentResponse = await DefaultApi.InstrumentsClient().Get(createInstrumentResponse.Id);
            retrieveInstrumentResponse.ShouldNotBeNull();
            retrieveInstrumentResponse.Name.ShouldBe("New Name");
            retrieveInstrumentResponse.ExpiryMonth.ShouldBe(12);
            retrieveInstrumentResponse.ExpiryYear.ShouldBe(2026);
        }

        [Fact]
        private async Task ShouldCreateAndDeleteInstrument()
        {
            var createInstrumentResponse = await CreateTokenInstrument();

            await DefaultApi.InstrumentsClient().Delete(createInstrumentResponse.Id);

            await AssertNotFound(DefaultApi.InstrumentsClient().Get(createInstrumentResponse.Id));
        }

        private async Task<CreateInstrumentResponse> CreateTokenInstrument()
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

            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();

            var request = new CreateInstrumentRequest
            {
                Token = cardTokenResponse.Token,
                Customer = new InstrumentCustomerRequest
                {
                    Email = "brucewayne@gmail.com",
                    Name = "Bruce Wayne",
                    Default = true,
                    Phone = new Phone
                    {
                        CountryCode = "+1",
                        Number = "4155552671"
                    }
                }
            };

            var createInstrumentResponse = await DefaultApi.InstrumentsClient().Create(request);
            createInstrumentResponse.ShouldNotBeNull();

            return createInstrumentResponse;
        }
    }
}