using Checkout.Accounts.Regional.US;
using Checkout.Common;
using Checkout.Instruments;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [Fact(Skip = "unavailable")]
        public async Task ShouldCreateAndGetEntityUSCompany()
        {
            string randomReference = RandomString(15);
            
            var onboardEntityRequest = new OnboardEntityRequest
            {
                Reference = randomReference,
                ContactDetails = new ContactDetails
                {
                    Phone = new AccountPhone
                    {
                        Number = "12345678"
                    },
                    EmailAddresses = new EntityEmailAddresses
                    {
                        Primary = "admin@superhero1234.com"
                    }
                },
                Profile = new Profile
                {
                    Urls = new List<string>
                    {
                        {"https://www.superheroexample.com"}
                    },
                    Mccs = new List<string>
                    {
                        {"5311"}
                    },
                    DefaultHoldingCurrency = Currency.USD,
                    HoldingCurrencies = new List<Currency>
                    {
                        Currency.GBP,
                        Currency.USD
                    }

                },
                Company = new USCompany
                {
                    BusinessRegistrationNumber = "12345678",
                    BusinessType = USBusinessType.PrivateCorporation,
                    LegalName = "UsCompany",
                    TradingName = "UsCompany",
                    PrincipalAddress = new Address
                    {
                        AddressLine1 = "123 Main St",
                        AddressLine2 = "Apt 101",
                        City = "New York",
                        State = "NY",
                        Zip = "10001",
                        Country = CountryCode.US
                    },
                },
                
            };
            
            OnboardEntityResponse entityResponse = await DefaultApi.AccountsClient().CreateEntity(onboardEntityRequest);
            
            entityResponse.ShouldNotBeNull();

            var entityDetailsResponse = await DefaultApi.AccountsClient().GetEntity(entityResponse.Id);
            
            entityDetailsResponse.ShouldNotBeNull();
            entityDetailsResponse.Company.BusinessType.ShouldBeOfType<USBusinessType>();
        }

        [Fact (Skip = "Not available")]
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
                        Zip = "WIT 4TJ",
                        Country = CountryCode.ES
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
        
        [Fact (Skip = "Not available")]
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
                        Zip = "WIT 4TJ",
                        Country = CountryCode.ES
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
                        new Representative
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Address = GetAddress(),
                            Identification = new Identification { NationalIdNumber = "AB123456C", }
                        }
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

            var instrumentResponse = await api.AccountsClient().CreatePaymentInstrument(entityResponse.Id, instrumentRequest);
            instrumentResponse.ShouldNotBeNull();
            instrumentResponse.Id.ShouldNotBeNull();

            var instrumentDetails = await api.AccountsClient().RetrievePaymentInstrumentDetails(entityResponse.Id, instrumentResponse.Id);
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
        private async Task ShouldCreateAndRetrievePaymentInstrumentUSCompany()
        {
            CheckoutApi api = GetAccountsCheckoutApi();

            var entityRequest = new OnboardEntityRequest
            {
                Reference = RandomString(15),
                ContactDetails = BuildContactDetails(),
                Profile = BuildProfile(),
                Company = new USCompany()
                {
                    BusinessRegistrationNumber = "01234567",
                    BusinessType = USBusinessType.PrivateCorporation,
                    LegalName = "Super Hero Masks Inc.",
                    TradingName = "Super Hero Masks",
                    PrincipalAddress = GetAddress(),
                    RegisteredAddress = GetAddress(),
                    Representatives = new List<Representative>
                    {
                        new Representative
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Address = GetAddress(),
                            Identification = new Identification { NationalIdNumber = "AB123456C", }
                        }
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

            var instrumentResponse = await api.AccountsClient().CreatePaymentInstrument(entityResponse.Id, instrumentRequest);
            instrumentResponse.ShouldNotBeNull();
            instrumentResponse.Id.ShouldNotBeNull();
            
            entityDetailsResponse.ShouldNotBeNull();

            var instrumentDetails = await api.AccountsClient().RetrievePaymentInstrumentDetails(entityResponse.Id, instrumentResponse.Id);
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
                Phone = new AccountPhone { Number = "2345678910" },
                EmailAddresses = new EntityEmailAddresses { Primary = GenerateRandomEmail() }
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