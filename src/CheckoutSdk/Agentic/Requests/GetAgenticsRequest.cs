namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Get Agentics Request
    /// </summary>
    public class GetAgenticsRequest
    {
        /// <summary>
        /// The number of items to skip
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// The maximum number of items to return
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Filter by active status
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Filter by name (partial match)
        /// </summary>
        public string NameFilter { get; set; }

        /// <summary>
        /// Filter by AI model
        /// </summary>
        public string AiModelFilter { get; set; }

        /// <summary>
        /// Sort field
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Sort direction (asc/desc)
        /// </summary>
        public string SortDirection { get; set; }
    }
}