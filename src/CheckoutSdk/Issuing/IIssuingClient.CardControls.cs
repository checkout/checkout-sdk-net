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
        Task<AbstractCardControlResponse> CreateCardControl(
            AbstractCardControlRequest abstractCardControlRequest,
            CancellationToken cancellationToken = default);

        Task<CardControlsQueryResponse> GetCardControls(CardControlQueryTarget query,
            CancellationToken cancellationToken = default);

        Task<AbstractCardControlResponse> GetCardControlDetails(string controlId,
            CancellationToken cancellationToken = default);

        Task<AbstractCardControlResponse> UpdateCardControl(string controlId,
            AbstractCardControlUpdate cardControlUpdate,
            CancellationToken cancellationToken = default);

        Task<IdResponse> RemoveCardControl(string controlId,
            CancellationToken cancellationToken = default);
    }
}