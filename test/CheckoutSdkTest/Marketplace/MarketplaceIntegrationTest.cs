using Checkout.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Marketplace.Test
{
    public class MarketplaceIntegrationTest : SandboxTestFixture
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public MarketplaceIntegrationTest() : base(PlatformType.FourOAuth)
        {
        }

        [Fact]
        public async Task ShouldCreateGetAndUpdateOnboardEntity()
        {
            string randomReference = RandomString(15);
            OnboardEntityRequest onboardEntityRequest = new OnboardEntityRequest()
            {
                Reference = randomReference,
                ContactDetails = new ContactDetails
                {
                    Phone = new Phone
                    {
                        Number = "2345678910"
                    }
                },
                Profile = new Profile
                {
                    Urls = new List<string>
                    {
                        "https://www.superheroexample.com"
                    },
                    Mccs = new List<string>
                    {
                        "0742"
                    }
                },
                Individual = new Individual
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    TradingName = "Batman's Super Hero Masks",
                    RegisteredAddress = new Address
                    {
                        AddressLine1 = "Checkout.com",
                        AddressLine2 = "90 Tottenham Court Road",
                        City = "London",
                        State = "London",
                        Zip = "WIT 4TJ",
                        Country = CountryCode.GB
                    },
                    NationalTaxId = "TAX123456",
                    DateOfBirth = new DateOfBirth
                    {
                        Day = 5,
                        Month = 6,
                        Year = 1996
                    },
                    Identification = new Identification
                    {
                        NationalIdNumber = "AB123456C"
                    }
                }
            };

            OnboardEntityResponse entityResponse = await FourApi.MarketplaceClient().CreateEntity(onboardEntityRequest);

            entityResponse.ShouldNotBeNull();

            string entityId = entityResponse.Id;

            entityId.ShouldNotBeNullOrEmpty();
            entityResponse.Reference.ShouldBe(randomReference);

            OnboardEntityDetailsResponse entityDetailsResponse = await FourApi.MarketplaceClient().GetEntity(entityId);

            entityDetailsResponse.ShouldNotBeNull();
            entityDetailsResponse.Id.ShouldBe(entityId);
            entityDetailsResponse.Reference.ShouldBe(randomReference);
            entityDetailsResponse.ContactDetails.ShouldNotBeNull();
            entityDetailsResponse.ContactDetails.Phone.ShouldNotBeNull();
            entityDetailsResponse.ContactDetails.Phone.Number.ShouldBe(onboardEntityRequest.ContactDetails.Phone.Number);
            entityDetailsResponse.Individual.ShouldNotBeNull();
            entityDetailsResponse.Individual.FirstName.ShouldBe(onboardEntityRequest.Individual.FirstName);
            entityDetailsResponse.Individual.LastName.ShouldBe(onboardEntityRequest.Individual.LastName);
            entityDetailsResponse.Individual.TradingName.ShouldBe(onboardEntityRequest.Individual.TradingName);
            entityDetailsResponse.Individual.NationalTaxId.ShouldBe(onboardEntityRequest.Individual.NationalTaxId);

            onboardEntityRequest.Individual.FirstName = "Jhon";

            OnboardEntityResponse updatedEntityResponse = await FourApi.MarketplaceClient().UpdateEntity(onboardEntityRequest, entityId);

            updatedEntityResponse.ShouldNotBeNull();

            OnboardEntityDetailsResponse verifyUpdated = await FourApi.MarketplaceClient().GetEntity(entityId);

            verifyUpdated.ShouldNotBeNull();
            onboardEntityRequest.Individual.FirstName.ShouldBe(verifyUpdated.Individual.FirstName);
        }

        [Fact]
        private async Task ShouldUploadMarketplaceFile()
        {
            var workingDirectory = System.Environment.CurrentDirectory;

            int index = workingDirectory.IndexOf("CheckoutSdkTest") + 15;

            var path = workingDirectory.Remove(index, workingDirectory.Count() - index);

            FileStream file;

            if (File.Exists(path + "\\marketplace\\testfile.png"))
            {
                file = File.OpenRead(path + "\\marketplace\\testfile.png");
                path = file.Name;
                file.Close();
            }
            else if (File.Exists(path + "/marketplace/testfile.png"))
            {
                file = File.OpenRead(path + "/marketplace/testfile.png");
                path = file.Name;
                file.Close();
            }

            path.ShouldNotBeEmpty();

            var fileRequest =
                new MarketplaceFileRequest(
                    path,
                    new ContentType("image/png"),
                    MarketplaceFilePurpose.Identification);

            IdResponse fileResponse = await FourApi.MarketplaceClient().SubmitFile(fileRequest);

            fileResponse.ShouldNotBeNull();
            fileResponse.Id.ShouldNotBeNull();
        }
    }
}