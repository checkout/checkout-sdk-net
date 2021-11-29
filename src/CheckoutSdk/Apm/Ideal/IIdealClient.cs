using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Ideal
{
    public interface IIdealClient
    {
        Task<IdealInfo> GetInfo(CancellationToken cancellationToken = default);

        Task<IssuerResponse> GetIssuers(CancellationToken cancellationToken = default);
    }
}