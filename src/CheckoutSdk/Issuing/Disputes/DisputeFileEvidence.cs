namespace Checkout.Issuing.Disputes
{
    /// <summary>
    /// File evidence for an Issuing dispute, referencing an uploaded file by ID.
    /// [Beta]
    /// </summary>
    public class DisputeFileEvidence
    {
        /// <summary>
        /// The unique identifier for an uploaded file.
        /// [Required]
        /// ^file_[a-z0-9]{26}$
        /// 31 characters
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// A brief description of the evidence.
        /// [Optional]
        /// </summary>
        public string Description { get; set; }
    }
}