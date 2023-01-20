using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Common;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Accounts
{
    public class AccountsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidPreviousSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<IApiClient> _apiFilesClient = new Mock<IApiClient>();
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly AccountsClient _accountsClient;

        public AccountsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory);
            _accountsClient =
                new AccountsClient(_apiClient.Object, _apiFilesClient.Object, configuration.Object);
        }

        [Fact]
        public async Task ShouldCreateEntity()
        {
            var onboardEntityResponse = new OnboardEntityResponse {Id = "Id"};

            _apiClient.Setup(x => x.Post<OnboardEntityResponse>("accounts/entities", It.IsAny<SdkAuthorization>(),
                    It.IsAny<object>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(onboardEntityResponse);

            var response = await _accountsClient.CreateEntity(new OnboardEntityRequest());

            response.ShouldNotBeNull();
            response.ShouldBe(onboardEntityResponse);
        }

        [Fact]
        private async Task ShouldGetEntity()
        {
            var responseObject = new OnboardEntityDetailsResponse {Id = "entity_id"};

            _apiClient
                .Setup(x =>
                    x.Get<OnboardEntityDetailsResponse>(
                        "accounts/entities/entity_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseObject);

            var response = await _accountsClient.GetEntity(responseObject.Id);

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        private async Task ShouldUpdateEntity()
        {
            var responseObject = new OnboardEntityResponse {Id = "entity_id", Reference = "A"};
            var request = new OnboardEntityRequest
            {
                Reference = "123",
                ContactDetails = new ContactDetails {Phone = new AccountPhone()},
                Profile = new Profile(),
                Company = new Company
                {
                    BusinessRegistrationNumber = "123",
                    BusinessType = BusinessType.UnincorporatedAssociation,
                    LegalName = "LEGAL",
                    TradingName = "TRADING",
                    PrincipalAddress = new Address(),
                    RegisteredAddress = new Address(),
                    Representatives = new List<Representative>
                    {
                        new Representative
                        {
                            Id = "1203",
                            FirstName = "first",
                            LastName = "last",
                            Address = new Address(),
                            Identification = new Identification(),
                            Phone = new AccountPhone(),
                            DateOfBirth = new DateOfBirth {Day = 1, Month = 1, Year = 2000},
                            PlaceOfBirth = new PlaceOfBirth {Country = CountryCode.AF},
                            Roles = new List<EntityRoles> {EntityRoles.Ubo}
                        }
                    },
                    Document = new EntityDocument(),
                    FinancialDetails = new EntityFinancialDetails
                    {
                        AnnualProcessingVolume = 1,
                        AverageTransactionValue = 1,
                        HighestTransactionValue = 1,
                        Documents = new EntityFinancialDocuments
                        {
                            BankStatement = new EntityDocument(),
                            FinancialStatement = new EntityDocument()
                        }
                    }
                },
                Individual = null
            };

            _apiClient
                .Setup(x =>
                    x.Put<OnboardEntityResponse>(
                        "accounts/entities/entity_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(responseObject);

            var response = await _accountsClient.UpdateEntity(
                responseObject.Id,
                request);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(responseObject.Id);
            response.Reference.ShouldBe(responseObject.Reference);
        }

        [Fact]
        private async Task ShouldCreatePaymentInstrumentDeprecated()
        {
            _apiClient
                .Setup(x =>
                    x.Post<EmptyResponse>(
                        "accounts/entities/entity_id/instruments",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(() => new EmptyResponse());

            var response = await _accountsClient.CreatePaymentInstrument(
                "entity_id",
                new AccountsPaymentInstrument
                {
                    AccountNumber = "324445",
                    AccountHolder = new AccountsIndividualAccountHolder
                    {
                        Type = AccountHolderType.Individual,
                        TaxId = "123",
                        DateOfBirth = new DateOfBirth(),
                        CountryOfBirth = CountryCode.AC,
                        ResidentialStatus = "status",
                        BillingAddress = new Address(),
                        Phone = new AccountPhone(),
                        Identification = new AccountHolderIdentification(),
                        Email = "account@checkout.com",
                        FirstName = "First",
                        LastName = "Last"
                    }
                });

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldSubmitFile()
        {
            //Arrange
            var responseObject = new IdResponse {Id = "Id"};

            _apiFilesClient
                .Setup(x =>
                    x.Post<IdResponse>(
                        It.IsAny<string>(),
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<MultipartFormDataContent>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseObject);

            var response =
                await _accountsClient.SubmitFile(new AccountsFileRequest
                {
                    File = "./Resources/checkout.jpeg",
                    ContentType = null,
                    Purpose = AccountsFilePurpose.Identification
                });

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        private async Task ShouldUpdatePayoutSchedule()
        {
            var responseAsync = new EmptyResponse();

            _apiClient
                .Setup(x =>
                    x.Put<EmptyResponse>(
                        "accounts/entities/entity_id/payout-schedules",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseAsync);

            var response =
                await _accountsClient.UpdatePayoutSchedule("entity_id", Currency.AED, new UpdateScheduleRequest());

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetrievePayoutSchedule()
        {
            var responseAsync = new GetScheduleResponse();

            _apiClient
                .Setup(x =>
                    x.Get<GetScheduleResponse>(
                        "accounts/entities/entity_id/payout-schedules",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseAsync);

            var response = await _accountsClient.RetrievePayoutSchedule(
                "entity_id");

            response.ShouldNotBeNull();
        }
        
        [Fact]
        private async Task ShouldRetrievePaymentInstrumentDetails()
        {
            var responseAsync = new PaymentInstrumentDetailsResponse();

            _apiClient
                .Setup(x =>
                    x.Get<PaymentInstrumentDetailsResponse>(
                        "accounts/entities/entity_id/payment-instruments/instrument_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseAsync);

            var response = await _accountsClient.RetrievePaymentInstrumentDetails(
                "entity_id", "instrument_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCreatePaymentInstrument()
        {
            PaymentInstrumentRequest request = new PaymentInstrumentRequest();
            IdResponse responseAsync = new IdResponse();

            _apiClient
                .Setup(x =>
                    x.Post<IdResponse>(
                        "accounts/entities/entity_id/payment-instruments",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<PaymentInstrumentRequest>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseAsync);

            var response =
                await _accountsClient.CreatePaymentInstrument("entity_id", request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdatePaymentInstrument()
        {
            UpdatePaymentInstrumentRequest request = new UpdatePaymentInstrumentRequest();
            IdResponse responseAsync = new IdResponse();

            _apiClient
                .Setup(x =>
                    x.Patch<IdResponse>(
                        "accounts/entities/entity_id/payment-instruments/instrument_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<UpdatePaymentInstrumentRequest>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseAsync);

            var response =
                await _accountsClient.UpdatePaymentInstrument("entity_id", "instrument_id", request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldQueryPaymentInstruments()
        {
            PaymentInstrumentsQuery request = new PaymentInstrumentsQuery();
            PaymentInstrumentQueryResponse responseAsync = new PaymentInstrumentQueryResponse();

            _apiClient
                .Setup(x =>
                    x.Query<PaymentInstrumentQueryResponse>(
                        "accounts/entities/entity_id/payment-instruments",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<PaymentInstrumentsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(responseAsync);

            var response =
                await _accountsClient.QueryPaymentInstruments("entity_id", request);

            response.ShouldNotBeNull();
        }
    }
}