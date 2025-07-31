namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.MerchantInitiatedChannelData
{
    /// <summary>
    /// merchant_initiated channel_data Class
    /// The information gathered from the environment used to initiate the session
    /// </summary>
    public class MerchantInitiatedChannelData : AbstractChannelData
    {
        /// <summary>
        /// Initializes a new instance of the MerchantInitiatedChannelData class.
        /// </summary>
        public MerchantInitiatedChannelData() : base(ChannelDataType.MerchantInitiated)
        {
        }

        /// <summary>
        /// [Required]
        /// </summary>
        public RequestType? RequestType { get; set; }

    }
}
