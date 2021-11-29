using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Common;

namespace Checkout.Files
{
    public class FilesClient : AbstractClient, IFilesClient
    {
        private const string Files = "files";

        private readonly IDictionary<string, string> _allowedMimeMapping = new Dictionary<string, string>
        {
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".png", "image/png"},
            {".pdf", "application/pdf"}
        };

        public FilesClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<IdResponse> SubmitFile(string pathToFile, string purpose,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("pathToFile", pathToFile, "purpose", purpose);
            var dataContent = CreateMultipartRequest(pathToFile, purpose);
            return ApiClient.Post<IdResponse>(Files, SdkAuthorization(), dataContent, cancellationToken);
        }

        public Task<FileDetailsResponse> GetFileDetails(string fileId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("fileId", fileId);
            return ApiClient.Get<FileDetailsResponse>(BuildPath(Files, fileId), SdkAuthorization(), cancellationToken);
        }

        private MultipartFormDataContent CreateMultipartRequest(string pathToFile, string purpose)
        {
            var fileInfo = new FileInfo(pathToFile);
            if (!fileInfo.Exists)
            {
                throw new CheckoutFileException(
                    $"The file in {pathToFile} does not exist");
            }

            if (!_allowedMimeMapping.TryGetValue(fileInfo.Extension.ToLower(), out string mediaType))
            {
                throw new CheckoutFileException(
                    $"The file type {fileInfo.Extension} cannot be uploaded.\n Supported file types: JPG/JPEG, PNG and PDF.");
            }

            var fileFieldStreamContent =
                new StreamContent(new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read));
            fileFieldStreamContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            var purposeFieldStringContent = new StringContent(purpose);
            var dataContent = new MultipartFormDataContent();
            dataContent.Add(fileFieldStreamContent, "file", fileInfo.Name);
            dataContent.Add(purposeFieldStringContent, "purpose");
            return dataContent;
        }
    }
}