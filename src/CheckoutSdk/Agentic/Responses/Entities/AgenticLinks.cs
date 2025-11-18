namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Links for agentic purchase intent operations
    /// </summary>
    public class AgenticLinks
    {
        /// <summary>
        /// Link to self
        /// </summary>
        public string Self { get; set; }

        /// <summary>
        /// Link to create credentials
        /// </summary>
        public string CreateCredentials { get; set; }

        /// <summary>
        /// Link to update
        /// </summary>
        public string Update { get; set; }

        /// <summary>
        /// Link to cancel
        /// </summary>
        public string Cancel { get; set; }
    }
}