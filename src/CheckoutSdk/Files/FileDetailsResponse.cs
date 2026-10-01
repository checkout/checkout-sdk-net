using Checkout.Accounts;
using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Files
{
    /// <summary>
    /// The details of an uploaded file. Returned by two endpoints with different fields:
    /// <list type="bullet">
    /// <item><description>GET /entities/{entityId}/files/{fileId} (<see cref="IAccountsClient.RetrieveFile"/>):
    /// id, status, status_reasons, size, mime_type, uploaded_on, purpose.</description></item>
    /// <item><description>GET /files/{file_id} (<see cref="IFilesClient.GetFileDetails"/>): id, filename,
    /// purpose, size, uploaded_on.</description></item>
    /// </list>
    /// </summary>
    public class FileDetailsResponse : Resource
    {
        /// <summary>
        /// The ID of the file.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The current status of the file. Entity files only.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// If <see cref="Status"/> is <c>invalid</c>, the reasons why the file was invalid; otherwise
        /// null. Entity files only.
        /// </summary>
        public IList<string> StatusReasons { get; set; }

        /// <summary>
        /// The size of the file: in KB for entity files, in bytes for GET /files/{file_id}.
        /// The API sends a number; it is exposed as a string here and parses with
        /// <c>long.Parse</c> or <c>decimal.Parse</c>.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The MIME type of the file. Entity files only.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// The date and time the file was uploaded, in ISO 8601 UTC format.
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? UploadedOn { get; set; }

        /// <summary>
        /// The filename, including its extension. GET /files/{file_id} only.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// The purpose of the file, as provided in the initial request.
        /// A value that <see cref="AccountsFilePurpose"/> does not define (for example the
        /// <c>arbitration_evidence</c> and <c>submitted_evidence</c> values of GET /files/{file_id})
        /// deserializes to the enum's default, <see cref="AccountsFilePurpose.AdditionalDocument"/>.
        /// </summary>
        public AccountsFilePurpose Purpose { get; set; }
    }
}