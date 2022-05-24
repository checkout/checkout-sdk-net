using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Common;
using Shouldly;
using Xunit;

namespace Checkout.Apm.Klarna
{
    public class KlarnaIntegrationTest : SandboxTestFixture
    {
        public KlarnaIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetKlarnaSession()
        {
            var creditSessionRequest = new CreditSessionRequest()
            {
                PurchaseCountry = CountryCode.GB,
                Currency = Currency.GBP,
                Locale = "en-GB",
                Amount = 1000,
                TaxAmount = 1,
                Products = new List<KlarnaProduct>()
                {
                    new KlarnaProduct()
                    {
                        Name = "Brown leather belt",
                        Quantity = 1,
                        UnitPrice = 1000,
                        TaxRate = 0,
                        TotalAmount = 1000,
                        TotalTaxAmount = 0
                    }
                }
            };

            var creditSessionResponse = await DefaultApi.KlarnaClient().CreateCreditSession(creditSessionRequest);

            creditSessionResponse.ShouldNotBeNull();
            creditSessionResponse.SessionId.ShouldNotBeNullOrEmpty();
            creditSessionResponse.ClientToken.ShouldNotBeNullOrEmpty();
            creditSessionResponse.PaymentMethodCategories.ShouldNotBeNull();
            //creditSessionResponse.PaymentMethodCategories.Count.ShouldBePositive();

            foreach (var paymentMethodCategory in creditSessionResponse.PaymentMethodCategories)
            {
                paymentMethodCategory.Name.ShouldNotBeNullOrEmpty();
                paymentMethodCategory.Identifier.ShouldNotBeNullOrEmpty();
                paymentMethodCategory.AssetUrls.ShouldNotBeNull();
                paymentMethodCategory.AssetUrls.Descriptive.ShouldNotBeNullOrEmpty();
                paymentMethodCategory.AssetUrls.Standard.ShouldNotBeNullOrEmpty();
            }

            var creditSession = await DefaultApi.KlarnaClient().GetCreditSession(creditSessionResponse.SessionId);

            creditSession.ShouldNotBeNull();
            creditSession.ClientToken.ShouldNotBeNullOrEmpty();
            creditSession.PurchaseCountry.ShouldNotBeNull();
            creditSession.Currency.ShouldNotBeNull();
            creditSession.Locale.ShouldNotBeNull();
            creditSession.Amount.ShouldNotBeNull();
            creditSession.TaxAmount.ShouldNotBeNull();
            creditSession.Products.ShouldNotBeNull();
            foreach (var creditSessionProduct in creditSession.Products)
            {
                creditSessionProduct.Name.ShouldNotBeNullOrEmpty();
                creditSessionProduct.Quantity.ShouldNotBeNull();
                creditSessionProduct.UnitPrice.ShouldNotBeNull();
                creditSessionProduct.TaxRate.ShouldNotBeNull();
                creditSessionProduct.TotalAmount.ShouldNotBeNull();
                creditSessionProduct.TotalTaxAmount.ShouldNotBeNull();
            }
        }
    }
}