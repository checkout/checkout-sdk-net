namespace Checkout.Authentication.Standalone.Common.InitialTransaction
{
    /// <summary>
    /// initial_transaction
    /// Details of a previous transaction
    /// </summary>
    public class InitialTransaction
    {
        /// <summary>
        /// The Access Control Server (ACS) transaction ID for a previously authenticated transaction
        /// [Optional]
        /// &gt;= 36
        /// </summary>
        public string AcsTransactionId { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public AuthenticationMethodType? AuthenticationMethod { get; set; }

        /// <summary>
        /// The timestamp of the previous authentication, in ISO 8601 format.
        /// [Optional]
        /// </summary>
        public string AuthenticationTimestamp { get; set; }

        /// <summary>
        /// Data that documents and supports a specific authentication process
        /// [Optional]
        /// &lt;= 2048
        /// </summary>
        public string AuthenticationData { get; set; }

        /// <summary>
        /// The ID for a previous session, which is used for retrieve the initial transaction's properties
        /// [Optional]
        /// 30 characters
        /// </summary>
        public string InitialSessionId { get; set; }
    }
}