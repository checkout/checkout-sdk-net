using System.Threading.Tasks;

namespace Checkout.Sources
{
    /// <summary>
    /// Defines the operations available on the Checkout.com Sources API.
    /// </summary>
    public interface ISourcesClient
    {
        /// <summary>
        /// Request a source.
        /// </summary>
        /// <param name="sourceRequest">The source details such as type and billing address.</param>
        /// <returns>A task that upon completion contains the source response.</returns>
        Task<SourceResponse> RequestAsync(SourceRequest sourceRequest);
    }
}
