using System.Threading;
using System.Threading.Tasks;
using Checkout.Common;

namespace Checkout.Files
{
    public interface IFilesClient
    {
        Task<IdResponse> SubmitFile(string pathToFile, string purpose, CancellationToken cancellationToken = default);

        Task<FileDetailsResponse> GetFileDetails(string fileId, CancellationToken cancellationToken = default);
    }
}