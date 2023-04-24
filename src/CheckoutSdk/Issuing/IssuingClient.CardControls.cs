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
        public Task<CardControlResponse> CreateCardControl(
            CardControlRequest cardControlRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardControlRequest", cardControlRequest);
            return ApiClient.Post<CardControlResponse>(
                BuildPath(IssuingPath, ControlsPath),
                SdkAuthorization(),
                cardControlRequest,
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

        public Task<CardControlResponse> GetCardControlDetails(string controlId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlId", controlId);
            return ApiClient.Get<CardControlResponse>(
                BuildPath(IssuingPath, ControlsPath, controlId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<CardControlResponse> UpdateCardControl(string controlId,
            UpdateCardControlRequest updateCardControlRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("controlId", controlId, "updateCardControlRequest", updateCardControlRequest);
            return ApiClient.Put<CardControlResponse>(
                BuildPath(IssuingPath, ControlsPath, controlId),
                SdkAuthorization(),
                updateCardControlRequest,
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