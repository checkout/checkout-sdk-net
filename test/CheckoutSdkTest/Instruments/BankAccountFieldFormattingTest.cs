using Checkout.Common;
using Checkout.Instruments.Get;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Instruments
{
    public class BankAccountFieldFormattingTest : SandboxTestFixture
    {
        public BankAccountFieldFormattingTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldGetBankAccountFieldFormatting()
        {
            BankAccountFieldQuery query = new BankAccountFieldQuery
            {
                AccountHolderType = AccountHolderType.Individual, PaymentNetwork = PaymentNetwork.Local
            };

            var response = await DefaultApi.InstrumentsClient()
                .GetBankAccountFieldFormatting(CountryCode.GB, Currency.GBP, query);

            response.ShouldNotBeNull();
            response.Sections.ShouldNotBeNull();
            response.Sections.ShouldNotBeEmpty();

            foreach (var section in response.Sections)
            {
                section.Name.ShouldNotBeNull();
                section.Fields.ShouldNotBeNull();
                section.Fields.ShouldNotBeEmpty();

                foreach (var field in section.Fields)
                {
                    field.Id.ShouldNotBeNull();
                    field.Display.ShouldNotBeNull();
                    field.Type.ShouldNotBeNull();
                }
            }
        }
    }
}