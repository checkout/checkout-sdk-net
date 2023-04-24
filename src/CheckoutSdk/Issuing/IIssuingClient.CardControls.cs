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
    public partial interface IIssuingClient
    {
        Task<CardControlResponse> CreateCardControl(
            CardControlRequest cardControlRequest,
            CancellationToken cancellationToken = default);

        Task<CardControlsQueryResponse> GetCardControls(CardControlQueryTarget query,
            CancellationToken cancellationToken = default);

        Task<CardControlResponse> GetCardControlDetails(string controlId,
            CancellationToken cancellationToken = default);

        Task<CardControlResponse> UpdateCardControl(string controlId,
            UpdateCardControlRequest updateCardControlRequest,
            CancellationToken cancellationToken = default);

        Task<IdResponse> RemoveCardControl(string controlId,
            CancellationToken cancellationToken = default);
    }
}