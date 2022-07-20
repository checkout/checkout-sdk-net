using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    public class InstrumentsClient : AbstractClient, IInstrumentsClient
    {
        private const string InstrumentsPath = "instruments";

        public InstrumentsClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<CreateInstrumentResponse> Create(CreateInstrumentRequest createInstrumentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createInstrumentRequest", createInstrumentRequest);
            return ApiClient.Post<CreateInstrumentResponse>(
                InstrumentsPath,
                SdkAuthorization(),
                createInstrumentRequest,
                cancellationToken);
        }

        public Task<GetInstrumentResponse> Get(string instrumentId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("instrumentId", instrumentId);
            return ApiClient.Get<GetInstrumentResponse>(BuildPath(InstrumentsPath, instrumentId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<UpdateInstrumentResponse> Update(string instrumentId,
            UpdateInstrumentRequest updateInstrumentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("instrumentId", instrumentId, "updateInstrumentRequest",
                updateInstrumentRequest);
            return ApiClient.Patch<UpdateInstrumentResponse>(BuildPath(InstrumentsPath, instrumentId),
                SdkAuthorization(),
                updateInstrumentRequest,
                cancellationToken);
        }

        public Task<EmptyResponse> Delete(string instrumentId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("instrumentId", instrumentId);
            return ApiClient.Delete<EmptyResponse>(
                BuildPath(InstrumentsPath, instrumentId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<BankAccountFieldResponse> GetBankAccountFieldFormatting(CountryCode country, Currency currency,
            BankAccountFieldQuery bankAccountFieldQuery, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("country", country, "currency", currency, "bankAccountFieldQuery",
                bankAccountFieldQuery);
            return ApiClient.Query<BankAccountFieldResponse>(
                BuildPath("validation/bank-accounts", country.ToString(), currency.ToString()),
                SdkAuthorization(SdkAuthorizationType.OAuth), bankAccountFieldQuery, cancellationToken);
        }
    }
}