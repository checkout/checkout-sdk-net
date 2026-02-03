namespace Checkout.Authentication.Standalone.Common.Responses.Threeds.ErrorDetails
{
    /// <summary>
    /// error_details
    /// Provides additional information about the error returned.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// An error code identifying the type of issue.
        /// [Optional]
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// A code that specifies which 3D Secure component identified the error.
        /// [Optional]
        /// </summary>
        public string ErrorComponent { get; set; }

        /// <summary>
        /// Provides additional details about the issue.
        /// [Optional]
        /// &lt;= 2048
        /// </summary>
        public string ErrorDetail { get; set; }

        /// <summary>
        /// A description of the issue identified.
        /// [Optional]
        /// &lt;= 2048
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}