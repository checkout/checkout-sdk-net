using System.Threading;
using System.Threading.Tasks;

namespace Checkout.MetaData.Card
{
    public interface IMetaDataCardClient
    {
        Task<MetaDataCardResponse> RequestCardMetaData(MetaDataCardRequest metaDataCardRequest, CancellationToken cancellationToken = default);
    }
}