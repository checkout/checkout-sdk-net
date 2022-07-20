using Checkout.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Accounts
{
    public class AccountsIntegrationTest : SandboxTestFixture
    {
        private static readonly Random Random = new Random();

        public AccountsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        public async Task ShouldCreateGetAndUpdateOnboardEntity()
        {
            string randomReference = RandomString(15);
            OnboardEntityRequest onboardEntityRequest = new OnboardEntityRequest
            {
                Reference = randomReference,
                ContactDetails = new ContactDetails {Phone = new AccountPhone {Number = "2345678910"}},
                Profile =
                    new Profile
                    {
                        Urls = new List<string> {"https://www.superheroexample.com"},
                        Mccs = new List<string> {"0742"}
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
                    DateOfBirth = new DateOfBirth {Day = 5, Month = 6, Year = 1996},
                    Identification = new Identification {NationalIdNumber = "AB123456C"},
                },
            };

            OnboardEntityResponse entityResponse = await DefaultApi.AccountsClient().CreateEntity(onboardEntityRequest);

            entityResponse.ShouldNotBeNull();

            string entityId = entityResponse.Id;

            entityId.ShouldNotBeNullOrEmpty();
            entityResponse.Reference.ShouldBe(randomReference);

            OnboardEntityDetailsResponse entityDetailsResponse = await DefaultApi.AccountsClient().GetEntity(entityId);

            entityDetailsResponse.ShouldNotBeNull();
            entityDetailsResponse.Id.ShouldBe(entityId);
            entityDetailsResponse.Reference.ShouldBe(randomReference);
            entityDetailsResponse.ContactDetails.ShouldNotBeNull();
            entityDetailsResponse.ContactDetails.Phone.ShouldNotBeNull();
            entityDetailsResponse.ContactDetails.Phone.Number.ShouldBe(onboardEntityRequest.ContactDetails.Phone
                .Number);
            entityDetailsResponse.Individual.ShouldNotBeNull();
            entityDetailsResponse.Individual.FirstName.ShouldBe(onboardEntityRequest.Individual.FirstName);
            entityDetailsResponse.Individual.LastName.ShouldBe(onboardEntityRequest.Individual.LastName);
            entityDetailsResponse.Individual.TradingName.ShouldBe(onboardEntityRequest.Individual.TradingName);
            entityDetailsResponse.Individual.NationalTaxId.ShouldBe(onboardEntityRequest.Individual.NationalTaxId);

            onboardEntityRequest.Individual.FirstName = "John";

            OnboardEntityResponse updatedEntityResponse =
                await DefaultApi.AccountsClient().UpdateEntity(entityId, onboardEntityRequest);

            updatedEntityResponse.ShouldNotBeNull();
            updatedEntityResponse.HttpStatusCode.ShouldNotBeNull();
            updatedEntityResponse.ResponseHeaders.ShouldNotBeNull();

            OnboardEntityDetailsResponse verifyUpdated = await DefaultApi.AccountsClient().GetEntity(entityId);

            verifyUpdated.ShouldNotBeNull();
            onboardEntityRequest.Individual.FirstName.ShouldBe(verifyUpdated.Individual.FirstName);
        }

        [Fact]
        private async Task ShouldUploadAccountsFile()
        {
            var fileRequest =
                new AccountsFileRequest
                {
                    File = "./Resources/checkout.jpeg",
                    ContentType = new ContentType("image/png"),
                    Purpose = AccountsFilePurpose.Identification
                };

            IdResponse fileResponse = await DefaultApi.AccountsClient().SubmitFile(fileRequest);

            fileResponse.ShouldNotBeNull();
            fileResponse.Id.ShouldNotBeNull();
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}