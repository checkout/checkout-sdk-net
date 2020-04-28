using Checkout.Common;
using System;

namespace Checkout.Files
{
    /// <summary>
    /// Represents a <see cref="File"/>.
    /// </summary>
    public class File : Resource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the file.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the purpose of the file.
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time the file was uploaded on.
        /// </summary>
        public DateTime UploadedOn { get; set; }
    }
}
