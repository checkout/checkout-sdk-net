using Checkout.Instruments.Four.Get;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments.Four
{
    public class InstrumentsClient : AbstractClient, IInstrumentsClient
    {
        private const string InstrumentsPath = "instruments";

        public InstrumentsClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<T> Create<T>(Create.CreateInstrumentRequest createInstrumentRequest,
            CancellationToken cancellationToken = default) where T : Create.CreateInstrumentResponse
        {
            CheckoutUtils.ValidateParams("createInstrumentRequest", createInstrumentRequest);
            return ApiClient.Post<T>(
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

        public Task<Update.UpdateInstrumentResponse> Update(string instrumentId,
            Update.UpdateInstrumentRequest updateInstrumentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("instrumentId", instrumentId, "updateInstrumentRequest",
                updateInstrumentRequest);
            return ApiClient.Patch<Update.UpdateInstrumentResponse>(BuildPath(InstrumentsPath, instrumentId),
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