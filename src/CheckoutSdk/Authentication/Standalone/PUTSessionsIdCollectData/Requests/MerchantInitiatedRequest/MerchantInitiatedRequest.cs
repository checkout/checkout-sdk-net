namespace Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests.MerchantInitiatedRequest
{
    /// <summary>
    /// Update a session
    /// Update a session by providing information about the environment.
    /// </summary>
    public class MerchantInitiatedRequest : AbstractUpdateASessionRequest
    {
        /// <summary>
        /// Initializes a new instance of the MerchantInitiated class.
        /// </summary>
        public MerchantInitiatedRequest() : base(ChannelType.MerchantInitiated)
        {
        }

        /// <summary>
        /// [Required]
        /// </summary>
        public RequestType? RequestType { get; set; }
    }
}