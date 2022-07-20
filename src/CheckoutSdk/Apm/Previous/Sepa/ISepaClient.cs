using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Previous.Sepa
{
    public interface ISepaClient
    {
        Task<MandateResponse> GetMandate(string mandateId, CancellationToken cancellationToken = default);

        Task<SepaResource> CancelMandate(string mandateId, CancellationToken cancellationToken = default);

        Task<MandateResponse> GetMandateViaPpro(string mandateId, CancellationToken cancellationToken = default);

        Task<SepaResource> CancelMandateViaPpro(string mandateId, CancellationToken cancellationToken = default);
    }
}