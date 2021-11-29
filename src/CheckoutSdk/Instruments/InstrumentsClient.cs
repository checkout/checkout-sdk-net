using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    public class InstrumentsClient : AbstractClient, IInstrumentsClient
    {
        private const string InstrumentsPath = "instruments";

        public InstrumentsClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<RetrieveInstrumentResponse> Get(string instrumentId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("instrumentId", instrumentId);
            return ApiClient.Get<RetrieveInstrumentResponse>(BuildPath(InstrumentsPath, instrumentId),
                SdkAuthorization(),
                cancellationToken);
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

        public Task<object> Delete(string instrumentId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("instrumentId", instrumentId);
            return ApiClient.Delete<object>(
                BuildPath(InstrumentsPath, instrumentId),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}