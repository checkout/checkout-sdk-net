using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Checkout.Files
{
    /// <summary>
    /// Defines the <see cref="FileUploadMultipartFormDataContentRequest"/> as content for the <see cref="HttpClient.PostAsync(string, HttpContent)"/> file upload request.
    /// </summary>
    public class FileUploadMultipartFormDataContentRequest : MultipartFormDataContent
    {
        private const string fileFieldName = "file";
        private const string purposeFieldName = "purpose";
        // Enhancement request to File API team that makes explicitly setting Content-Type obsolete
        private readonly Dictionary<string, string> allowedMimeMapping = new Dictionary<string, string>()
        {
            {".jpg", "image/jpeg" },
            {".jpeg", "image/jpeg" },
            {".png", "image/png" },
            {".pdf", "application/pdf" }
        };
        /// <summary>
        /// Creates new multipart/form-data content from file path for uploading a file as dispute evidence.
        /// </summary>
        /// <param name="pathToFile">The path to the file to be provided as evidence.</param>
        public FileUploadMultipartFormDataContentRequest(string pathToFile, string purpose)
        {
            var fileInfo = new FileInfo(pathToFile);
            if (!fileInfo.Exists) throw new FileNotFoundException();
            // Enhancement request to File API team that makes explicitly setting Content-Type obsolete
            if (!allowedMimeMapping.TryGetValue(fileInfo.Extension.ToLower(), out string mediaType)) throw new IOException($"The file type {fileInfo.Extension} cannot be uploaded.\n Supported file types: JPG/JPEG, PNG and PDF.");

            FileFieldName = fileFieldName;
            FileFieldStreamContent = new StreamContent(new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read));
            // Enhancement request to File API team that makes explicitly setting Content-Type obsolete
            FileFieldStreamContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            PurposeFieldName = purposeFieldName;
            PurposeFieldStringContent = new StringContent(purpose);
            

            Add(FileFieldStreamContent, FileFieldName, fileInfo.Name);
            Add(PurposeFieldStringContent, PurposeFieldName);
        }

        /// <summary>
        /// Gets or sets the name of the "file" field.
        /// </summary>
        public string FileFieldName { get; private set; }

        /// <summary>
        /// Gets or sets the StreamContent of the "file" field.
        /// </summary>
        public StreamContent FileFieldStreamContent { get; private set; }

        /// <summary>
        /// Gets or sets the name of the "purpose" field.
        /// </summary>
        public string PurposeFieldName { get; private set; }

        /// <summary>
        /// Gets or sets the StringContent of the "purpose" field.
        /// </summary>
        public StringContent PurposeFieldStringContent { get; private set; }
    }
}
