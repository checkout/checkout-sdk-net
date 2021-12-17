using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Forex
{
    public interface IForexClient
    {
        Task<QuoteResponse> RequestQuote(QuoteRequest quoteRequest, CancellationToken cancellationToken = default);
    }
}