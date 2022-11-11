using Checkout.Common;
using Checkout.Tokens;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.MetaData.Card
{
    public class MetaDataCardIntegrationTest : SandboxTestFixture
    {
        private const string BinNumberConstant = "01234567";
        private const string BinNumberErrorConstant = "01234567432435425324324342";
        private const string CardNumberConstant = "01234567890123456";
        private const string CardNumberErrorConstant = "01234";
        private const string PaymentSourceRequiredErrorConstant = "payment_source_required";
        
        public MetaDataCardIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardCardBasicExample()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                Number = CardNumberConstant
            };
            MetaDataCardResponse response = await DefaultApi.MetaDataCardClient().RequestCardMetaData(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(request.Bin);
            response.Scheme.ShouldNotBeNull();
            response.SchemeLocal.ShouldBeOfType<SchemeLocalType>();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(200);
            
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardNumberBasicExample()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                Bin = BinNumberConstant
            };
            MetaDataCardResponse response = await DefaultApi.MetaDataCardClient().RequestCardMetaData(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(request.Bin);
            response.Scheme.ShouldNotBeNull();
            response.SchemeLocal.ShouldBeOfType<SchemeLocalType>();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(200);
            
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardTokenBasicExample()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                TokenId = $"tok_{GetToken()}",
                Format = MetaDataCardFormatType.Basic
            };
            MetaDataCardResponse response = await DefaultApi.MetaDataCardClient().RequestCardMetaData(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(request.Bin);
            response.Scheme.ShouldNotBeNull();
            response.SchemeLocal.ShouldBeOfType<SchemeLocalType>();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(200);
            
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardCardPayoutsExample()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                Number = CardNumberConstant
            };
            MetaDataCardResponse response = await DefaultApi.MetaDataCardClient().RequestCardMetaData(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(request.Bin);
            response.Scheme.ShouldNotBeNull();
            response.SchemeLocal.ShouldBeOfType<SchemeLocalType>();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.CardPayouts.ShouldNotBe(null);
            response.CardPayouts.DomesticNonMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderNonMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.DomesticGambling.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderGambling.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.DomesticMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.HttpStatusCode.ShouldBe(200);
            
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardNumberPayoutsExample()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                Bin = BinNumberConstant
            };
            MetaDataCardResponse response = await DefaultApi.MetaDataCardClient().RequestCardMetaData(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(request.Bin);
            response.Scheme.ShouldNotBeNull();
            response.SchemeLocal.ShouldBeOfType<SchemeLocalType>();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.CardPayouts.ShouldNotBe(null);
            response.CardPayouts.DomesticNonMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderNonMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.DomesticGambling.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderGambling.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.DomesticMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.HttpStatusCode.ShouldBe(200);
            
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardTokenPayoutsExample()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                TokenId = $"tok_{GetToken()}",
                Format = MetaDataCardFormatType.CardPayouts
            };
            MetaDataCardResponse response = await DefaultApi.MetaDataCardClient().RequestCardMetaData(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(request.Bin);
            response.Scheme.ShouldNotBeNull();
            response.SchemeLocal.ShouldBeOfType<SchemeLocalType>();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.Issuer.ShouldNotBeNull();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.CardPayouts.ShouldNotBe(null);
            response.CardPayouts.DomesticNonMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderNonMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.DomesticGambling.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderGambling.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.DomesticMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.CardPayouts.CrossBorderMoneyTransfer.ShouldBeOfType<CardPayoutsTransferType>();
            response.HttpStatusCode.ShouldBe(200);
            
        }

        [Fact]
        private async Task ShouldRequestMetaDataCardCardError()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                Number = CardNumberErrorConstant
            };
            
            await CheckErrorItem(async () => await DefaultApi.MetaDataCardClient().RequestCardMetaData(request),
                PaymentSourceRequiredErrorConstant);
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardNumberError()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                Bin = BinNumberErrorConstant
            };
            
            await CheckErrorItem(async () => await DefaultApi.MetaDataCardClient().RequestCardMetaData(request),
                PaymentSourceRequiredErrorConstant);
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardTokenError()
        {
            MetaDataCardRequest request = new MetaDataCardRequest()
            {
                TokenId = $"tokError_{GetToken()}",
                Format = MetaDataCardFormatType.CardPayouts
            };
            
            await CheckErrorItem(async () => await DefaultApi.MetaDataCardClient().RequestCardMetaData(request),
                PaymentSourceRequiredErrorConstant);
        }
        
        [Fact]
        private async Task ShouldRequestMetaDataCardEmptyRequestError()
        {
            MetaDataCardRequest request = new MetaDataCardRequest();
            
            await CheckErrorItem(async () => await DefaultApi.MetaDataCardClient().RequestCardMetaData(request),
                PaymentSourceRequiredErrorConstant);
        }

        private async Task<string> GetToken()
        {
            Phone phone = new Phone {CountryCode = "44", Number = "020 222333"};

            Address billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            CardTokenRequest cardTokenRequest = new CardTokenRequest()
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var response = await DefaultApi.TokensClient().Request(cardTokenRequest);

            return response.Token;

        }
    }
}
