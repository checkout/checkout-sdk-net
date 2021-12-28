using Checkout.Common.Four;
using Checkout.Instruments.Four.Get;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Instruments.Four
{
    public class BankAccountFieldFormattingTest : SandboxTestFixture
    {
        public BankAccountFieldFormattingTest() : base(PlatformType.FourOAuth)
        {
        }

        [Fact]
        private async Task ShouldGetBankAccountFieldFormatting()
        {
            BankAccountFieldQuery query = new BankAccountFieldQuery()
            {
                AccountHolderType = PaymentSenderType.Individual,
                PaymentNetwork = PaymentNetwork.Local
            };

            var response = await FourApi.InstrumentsClient().GetBankAccountFieldFormatting(Common.CountryCode.GB, Common.Currency.GBP, query);

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