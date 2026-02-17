namespace Checkout.Issuing.Disputes
{
    /// <summary>
    /// Evidence for an Issuing dispute, containing file information and description.
    /// [Beta]
    /// </summary>
    public class DisputeEvidence
    {
        /// <summary>
        /// The complete file name, including the extension.
        /// [Required]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The base64-encoded string that represents a single JPG, PDF, TIFF, or ZIP file.
        /// ZIP files can contain multiple JPG, PDF, or TIFF files.
        /// [Required]
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// A brief description of the evidence.
        /// [Optional]
        /// </summary>
        public string Description { get; set; }
    }
}