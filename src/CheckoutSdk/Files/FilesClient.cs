using Checkout.Common;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Files
{
    public class FilesClient : AbstractClient, IFilesClient
    {
        private const string Files = "files";

        private static readonly IDictionary<string, string> AllowedMimeMapping = new Dictionary<string, string>
        {
            {".jpg", "image/jpeg"}, {".jpeg", "image/jpeg"}, {".png", "image/png"}, {".pdf", "application/pdf"}
        };

        private readonly IApiClient _filesApiClient;

        public FilesClient(
            IApiClient apiClient,
            IApiClient filesApiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration,
            SdkAuthorizationType.SecretKeyOrOAuth)
        {
            _filesApiClient = filesApiClient;
        }

        public Task<IdResponse> SubmitFile(string pathToFile, string purpose,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("pathToFile", pathToFile, "purpose", purpose);
            var dataContent = CreateMultipartRequest(pathToFile, purpose, "file");
            return ApiClient.Post<IdResponse>(Files, SdkAuthorization(), dataContent, cancellationToken);
        }

        public Task<FileDetailsResponse> GetFileDetails(string fileId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("fileId", fileId);
            return ApiClient.Get<FileDetailsResponse>(BuildPath(Files, fileId), SdkAuthorization(), cancellationToken);
        }

        public Task<IdResponse> SubmitFileToFilesApi(string pathToFile, string purpose,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("pathToFile", pathToFile, "purpose", purpose);
            if (_filesApiClient == null)
            {
                throw new CheckoutFileException(
                    "Files API is not enabled in this client. It must be enabled in CheckoutFourSdk configuration.");
            }

            var dataContent = CreateMultipartRequest(pathToFile, purpose, "path");
            return _filesApiClient.Post<IdResponse>(Files, SdkAuthorization(SdkAuthorizationType.OAuth), dataContent,
                cancellationToken);
        }

        private static MultipartFormDataContent CreateMultipartRequest(string pathToFile, string purpose,
            string multipartHeaderName)
        {
            var fileInfo = new FileInfo(pathToFile);
            if (!fileInfo.Exists)
            {
                throw new CheckoutFileException(
                    $"The file in {pathToFile} does not exist");
            }

            if (!AllowedMimeMapping.TryGetValue(fileInfo.Extension.ToLower(), out string mediaType))
            {
                throw new CheckoutFileException(
                    $"The file type {fileInfo.Extension} cannot be uploaded.\n Supported file types: JPG/JPEG, PNG and PDF.");
            }

            var fileFieldStreamContent =
                new StreamContent(new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read));
            fileFieldStreamContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            var purposeFieldStringContent = new StringContent(purpose);
            var dataContent = new MultipartFormDataContent();
            dataContent.Add(fileFieldStreamContent, multipartHeaderName, fileInfo.Name);
            dataContent.Add(purposeFieldStringContent, "purpose");
            return dataContent;
        }
    }
}