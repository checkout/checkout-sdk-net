using Checkout.Common;

namespace Checkout.Files
{
    /// <summary>
    /// Is the <see cref="UploadFileResponse"/> containing the <see cref="File.Id"/>.
    /// </summary>
    public class UploadFileResponse : Resource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the file.
        /// </summary>
        public string Id { get; set; }
    }
}
