using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.ContactDetails;
using Checkout.Accounts.Entities.Common.Documents;
using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Response;
using Checkout.Common;
using Checkout.Instruments;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection.Metadata;
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

        [Fact(Skip = "beta")]
        public async Task ShouldCreateHostedOnboardingInvitationRequest()
        {
            string randomReference = RandomString(15);
            var entityRequest = new OnboardEntityRequest
            {
                Reference = randomReference,
                IsDraft = false,
                ContactDetails = new ContactDetails { Invitee = new Invitee { Email = "admin@superhero1234.com" } }
            };

            var response = await DefaultApi.AccountsClient().CreateEntity(entityRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldBe(randomReference);
        }

        [Fact]
        public async Task ShouldCreateCompany()
        {
            string randomReference = RandomString(15);
            var entityRequest = new OnboardEntityRequest
            {
                Reference = randomReference,
                Draft = true,
                Profile =
                    new Profile
                    {
                        Urls = new List<string> { "http://example.com" },
                        Mccs = new List<string> { "4814" },
                        HoldingCurrencies = new List<Currency> { Currency.GBP }
                    },
                ContactDetails =
                    new ContactDetails
                    {
                        Phone = new Phone { CountryCode = "GI", Number = "656453654" },
                        EmailAddresses = new EmailAddresses { Primary = null },
                        Invitee = new Invitee { Email = null }
                    },
                Company =
                    new Company
                    {
                        LegalName = "string",
                        TradingName = "string",
                        BusinessRegistrationNumber = "AC123456",
                        DateOfIncorporation = new DateOfIncorporation { Day = 1, Month = 1, Year = 2001 },
                        PrincipalAddress = GetAddress(),
                        RegisteredAddress = GetAddress(),
                        BusinessType = BusinessType.IndividualOrSoleProprietorship
                    },
                ProcessingDetails =
                    new ProcessingDetails
                    {
                        SettlementCountry = "GB",
                        TargetCountries = new List<string> { "GB" },
                        Currency = Currency.GBP
                    },
                Documents = new Documents
                {
                    ArticlesOfAssociation =
                        new ArticlesOfAssociation()
                        {
                            Type = ArticlesOfAssociationType.ArticlesOfAssociation,
                            Front = "stringstringstringstringstrings"
                        },
                    ShareholderStructure = new ShareholderStructure()
                    {
                        Type = ShareholderStructureType.CertifiedShareholderStructure,
                        Front = "stringstringstringstringstrings"
                    },
                }
            };

            var response = await DefaultApi.AccountsClient().CreateEntity(entityRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldBe(randomReference);
        }

        [Fact]
        public async Task ShouldCreateGetAndUpdateOnboardEntity()
        {
            string randomReference = RandomString(15);
            OnboardEntityRequest onboardEntityRequest = new OnboardEntityRequest
            {
                Reference = randomReference,
                ContactDetails = BuildContactDetails(),
                Profile = BuildProfile(),
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
                        Zip = "W1T 4TJ",
                        Country = CountryCode.GB
                    },
                    NationalTaxId = "TAX123456",
                    DateOfBirth = new DateOfBirth { Day = 5, Month = 6, Year = 1996 },
                    Identification = new Identification { NationalIdNumber = "AB123456C" },
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
            entityDetailsResponse.ContactDetails.EmailAddresses.ShouldNotBeNull();
            entityDetailsResponse.ContactDetails.EmailAddresses.Primary.ShouldBe(onboardEntityRequest.ContactDetails
                .EmailAddresses.Primary);
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
        public async Task ShouldThrowConflictWhenCreatingExistingEntity()
        {
            string randomReference = RandomString(15);
            OnboardEntityRequest onboardEntityRequest = new OnboardEntityRequest
            {
                Reference = randomReference,
                ContactDetails = BuildContactDetails(),
                Profile = BuildProfile(),
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
                        Zip = "W1T 4TJ",
                        Country = CountryCode.GB
                    },
                    NationalTaxId = "TAX123456",
                    DateOfBirth = new DateOfBirth { Day = 5, Month = 6, Year = 1996 },
                    Identification = new Identification { NationalIdNumber = "AB123456C" },
                },
            };

            OnboardEntityResponse entityResponse = await DefaultApi.AccountsClient().CreateEntity(onboardEntityRequest);

            entityResponse.ShouldNotBeNull();

            string entityId = entityResponse.Id;

            entityId.ShouldNotBeNullOrEmpty();
            entityResponse.Reference.ShouldBe(randomReference);

            CheckoutApiException ex = await Assert.ThrowsAsync<CheckoutApiException>(() =>
                DefaultApi.AccountsClient().CreateEntity(onboardEntityRequest));

            ex.HttpStatusCode.ShouldBe(HttpStatusCode.Conflict);
            ex.ErrorDetails.ShouldNotBeNull();
            Assert.True(ex.ErrorDetails.ContainsKey("id"));
            ex.ErrorDetails["id"].ShouldBe(entityId);
        }

        [Fact(Skip = "unavailable")]
        public async Task ShouldCreateEntityUploadAndRetrieveFile()
        {
            var entityRequest = new OnboardEntityRequest
            {
                Reference = RandomString(15),
                Draft = true,
                ContactDetails =
                    new ContactDetails
                    {
                        Phone = new Phone { CountryCode = "GI", Number = "123456789" },
                        EmailAddresses = new EmailAddresses { Primary = "admin@example.com" }
                    },
                Profile =
                    new Profile
                    {
                        Urls = new List<string> { "http://example.com" },
                        Mccs = new List<string> { "4814" },
                        HoldingCurrencies = new List<Currency> { Currency.GBP }
                    },
                Company = new Company
                {
                    LegalName = "Test Company",
                    TradingName = "Test Trading",
                    BusinessRegistrationNumber = "AC123456",
                    DateOfIncorporation = new DateOfIncorporation { Day = 1, Month = 1, Year = 2020 },
                    PrincipalAddress = GetAddress(),
                    RegisteredAddress = GetAddress(),
                }
            };

            var entityResponse = await DefaultApi.AccountsClient().CreateEntity(entityRequest);

            entityResponse.ShouldNotBeNull();
            entityResponse.Id.ShouldNotBeNullOrEmpty();

            var fileRequest = new AccountsFileRequest { Purpose = AccountsFilePurpose.IdentityVerification };

            var uploadResponse = await DefaultApi.AccountsClient()
                .UploadFile(entityResponse.Id, fileRequest);

            uploadResponse.ShouldNotBeNull();
            uploadResponse.Id.ShouldNotBeNullOrEmpty();

            var retrievedFile = await DefaultApi.AccountsClient()
                .RetrieveFile(entityResponse.Id, uploadResponse.Id);

            retrievedFile.ShouldNotBeNull();
            retrievedFile.Id.ShouldBe(uploadResponse.Id);
        }

        [Fact]
        private async Task ShouldUploadAccountsFile()
        {
            await UploadFile();
        }

        [Fact]
        private async Task ShouldCreateAndRetrievePaymentInstrument()
        {
            CheckoutApi api = GetAccountsCheckoutApi();

            var entityRequest = new OnboardEntityRequest
            {
                Reference = RandomString(15),
                ContactDetails = BuildContactDetails(),
                Profile = BuildProfile(),
                Company = new Company
                {
                    BusinessRegistrationNumber = "01234567",
                    LegalName = "Super Hero Masks Inc.",
                    TradingName = "Super Hero Masks",
                    PrincipalAddress = GetAddress(),
                    RegisteredAddress = GetAddress(),
                    Representatives = new List<Representative>
                    {
                        new Representative { FirstName = "John", LastName = "Doe", Address = GetAddress(), }
                    }
                }
            };

            var entityResponse = await api.AccountsClient().CreateEntity(entityRequest);

            var file = await UploadFile();

            var instrumentRequest = new PaymentInstrumentRequest
            {
                Label = "Barclays",
                Type = InstrumentType.BankAccount,
                Currency = Currency.GBP,
                Country = CountryCode.GB,
                DefaultDestination = false,
                Document = new InstrumentDocument { Type = "bank_statement", FileId = file.Id },
                InstrumentDetails = new InstrumentDetailsFasterPayments
                {
                    AccountNumber = "12334454", BankCode = "050389"
                }
            };

            var instrumentResponse =
                await api.AccountsClient().CreatePaymentInstrument(entityResponse.Id, instrumentRequest);
            instrumentResponse.ShouldNotBeNull();
            instrumentResponse.Id.ShouldNotBeNull();

            var instrumentDetails = await api.AccountsClient()
                .RetrievePaymentInstrumentDetails(entityResponse.Id, instrumentResponse.Id);
            instrumentDetails.ShouldNotBeNull();
            instrumentDetails.Id.ShouldNotBeNull();
            instrumentDetails.Status.ShouldNotBeNull();
            instrumentDetails.Label.ShouldNotBeNull();
            instrumentDetails.Type.ShouldNotBeNull();
            instrumentDetails.Currency.ShouldNotBeNull();
            instrumentDetails.Country.ShouldNotBeNull();
            instrumentDetails.Document.ShouldNotBeNull();

            var queryResponse = await api.AccountsClient().QueryPaymentInstruments(entityResponse.Id);
            queryResponse.ShouldNotBeNull();
            queryResponse.Data.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCreateAndRetrievePaymentInstrumentCompany()
        {
            CheckoutApi api = GetAccountsCheckoutApi();

            var entityRequest = new OnboardEntityRequest
            {
                Reference = RandomString(15),
                ContactDetails = BuildContactDetails(),
                Profile = BuildProfile(),
                Company = new Company()
                {
                    BusinessRegistrationNumber = "01234567",
                    BusinessType = BusinessType.PrivateCorporation,
                    LegalName = "Super Hero Masks Inc.",
                    TradingName = "Super Hero Masks",
                    PrincipalAddress = GetAddress(),
                    RegisteredAddress = GetAddress(),
                    Representatives = new List<Representative>
                    {
                        new Representative { FirstName = "John", LastName = "Doe", Address = GetAddress(), }
                    }
                }
            };

            var entityResponse = await api.AccountsClient().CreateEntity(entityRequest);

            var entityDetailsResponse = await api.AccountsClient().GetEntity(entityResponse.Id);

            var file = await UploadFile();

            var instrumentRequest = new PaymentInstrumentRequest
            {
                Label = "Barclays",
                Type = InstrumentType.BankAccount,
                Currency = Currency.GBP,
                Country = CountryCode.GB,
                DefaultDestination = false,
                Document = new InstrumentDocument { Type = "bank_statement", FileId = file.Id },
                InstrumentDetails = new InstrumentDetailsFasterPayments
                {
                    AccountNumber = "12334454", BankCode = "050389"
                }
            };

            var instrumentResponse =
                await api.AccountsClient().CreatePaymentInstrument(entityResponse.Id, instrumentRequest);
            instrumentResponse.ShouldNotBeNull();
            instrumentResponse.Id.ShouldNotBeNull();

            entityDetailsResponse.ShouldNotBeNull();

            var instrumentDetails = await api.AccountsClient()
                .RetrievePaymentInstrumentDetails(entityResponse.Id, instrumentResponse.Id);
            instrumentDetails.ShouldNotBeNull();
            instrumentDetails.Id.ShouldNotBeNull();
            instrumentDetails.Status.ShouldNotBeNull();
            instrumentDetails.Label.ShouldNotBeNull();
            instrumentDetails.Type.ShouldNotBeNull();
            instrumentDetails.Currency.ShouldNotBeNull();
            instrumentDetails.Country.ShouldNotBeNull();
            instrumentDetails.Document.ShouldNotBeNull();

            var queryResponse = await api.AccountsClient().QueryPaymentInstruments(entityResponse.Id);
            queryResponse.ShouldNotBeNull();
            queryResponse.Data.ShouldNotBeNull();
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private static ContactDetails BuildContactDetails()
        {
            return new ContactDetails
            {
                Phone = new Phone { Number = "2345678910" },
                EmailAddresses = new EmailAddresses { Primary = GenerateRandomEmail() }
            };
        }

        private static Profile BuildProfile()
        {
            return new Profile
            {
                Urls = new List<string> { "https://www.superheroexample.com" }, Mccs = new List<string> { "0742" }
            };
        }

        private async Task<IdResponse> UploadFile()
        {
            var fileRequest =
                new AccountsFileRequest
                {
                    File = "./Resources/checkout.jpeg",
                    ContentType = new ContentType("image/png"),
                    Purpose = AccountsFilePurpose.BankVerification
                };

            IdResponse fileResponse = await DefaultApi.AccountsClient().SubmitFile(fileRequest);

            fileResponse.ShouldNotBeNull();
            fileResponse.Id.ShouldNotBeNull();

            return fileResponse;
        }

        private static CheckoutApi GetAccountsCheckoutApi()
        {
            return CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ACCOUNTS_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ACCOUNTS_CLIENT_SECRET"))
                .Scopes(OAuthScope.Accounts)
                .Build() as CheckoutApi;
        }
    }
}