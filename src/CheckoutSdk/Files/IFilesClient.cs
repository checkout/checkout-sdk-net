using Checkout.Common;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Files
{
    public interface IFilesClient
    {
        Task<IdResponse> SubmitFile(string pathToFile, string purpose, CancellationToken cancellationToken = default, string multipartFileHeaderName = null);

        Task<FileDetailsResponse> GetFileDetails(string fileId, CancellationToken cancellationToken = default);
    }
}