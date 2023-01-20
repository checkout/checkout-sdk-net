using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Sources.Previous
{
    public class SourcesTestIntegration : SandboxTestFixture
    {
        public SourcesTestIntegration() : base(PlatformType.Previous)
        {
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldCreateSepaSource()
        {
            var request = CreateSepaSourceRequest();

            var sourceResponse = await PreviousApi.SourcesClient().CreateSepaSource(request);
            sourceResponse.ShouldNotBeNull();
            sourceResponse.Type.ShouldBe(SourceType.Sepa);
            sourceResponse.Customer.ShouldNotBeNull();
            sourceResponse.Id.ShouldNotBeNullOrEmpty();
            sourceResponse.Links.Count.ShouldBe(2);
            sourceResponse.Links.ShouldContainKey("sepa:mandate-cancel");
            sourceResponse.Links.ShouldContainKey("sepa:mandate-get");
            sourceResponse.ResponseCode.ShouldBe("10000");
            sourceResponse.ResponseData.ShouldNotBeNull();
            sourceResponse.ResponseData.ShouldContainKey("mandate_reference");
        }

        private static SepaSourceRequest CreateSepaSourceRequest()
        {
            return new SepaSourceRequest
            {
                BillingAddress = new Address
                {
                    AddressLine1 = "Checkout.com",
                    AddressLine2 = "90 Tottenham Court Road",
                    City = "London",
                    State = "London",
                    Zip = "W1T 4TJ",
                    Country = CountryCode.GB
                },
                Reference = ".NET SDK test",
                Phone = new Phone
                {
                    CountryCode = "+1",
                    Number = "415 555 2671"
                },
                SourceData = new SourceData
                {
                    FirstName = "Marcus",
                    LastName = "Barrilius Maximus",
                    AccountIban = "DE68100100101234567895",
                    Bic = "PBNKDEFFXXX",
                    BillingDescriptor = ".NET SDK test",
                    MandateType = MandateType.Single
                }
            };
        }
    }
}