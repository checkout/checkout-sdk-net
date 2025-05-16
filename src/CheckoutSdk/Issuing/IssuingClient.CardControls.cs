using Checkout.Common;
using Checkout.Issuing.Controls.Requests.Create;
using Checkout.Issuing.Controls.Requests.Query;
using Checkout.Issuing.Controls.Requests.Update;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Issuing.Controls.Responses.Query;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<AbstractCardControlResponse> CreateCardControl(
            AbstractCardControlRequest abstractCardControlRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardControlRequest", abstractCardControlRequest);
            return ApiClient.Post<AbstractCardControlResponse>(
                BuildPath(IssuingPath, ControlsPath),
                SdkAuthorization(),
                abstractCardControlRequest,
                cancellationToken
            );
        }

        public Task<CardControlsQueryResponse> GetCardControls(CardControlQueryTarget query,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("query", query);
            return ApiClient.Query<CardControlsQueryResponse>(
                BuildPath(IssuingPath, ControlsPath),
                SdkAuthorization(),
                query,
                cancellationToken
            );
        }

        public Task<AbstractCardControlResponse> GetCardControlDetails(string controlId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlId", controlId);
            return ApiClient.Get<AbstractCardControlResponse>(
                BuildPath(IssuingPath, ControlsPath, controlId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<AbstractCardControlResponse> UpdateCardControl(string controlId,
            AbstractCardControlUpdate cardControlUpdate,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlId", controlId, "updateCardControlRequest", cardControlUpdate);
            return ApiClient.Put<AbstractCardControlResponse>(
                BuildPath(IssuingPath, ControlsPath, controlId),
                SdkAuthorization(),
                cardControlUpdate,
                cancellationToken
            );
        }

        public Task<IdResponse> RemoveCardControl(string controlId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlId", controlId);
            return ApiClient.Delete<IdResponse>(
                BuildPath(IssuingPath, ControlsPath, controlId),
                SdkAuthorization(),
                cancellationToken
            );
        }
    }
}