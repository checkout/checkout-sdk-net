namespace Checkout.NetworkTokens.PatchDelete.Requests
{
    public class PermanentlyDeleteANetworkTokenRequest
    {
        /// <summary> Who initiated/requested the deletion of the token. </summary>
        public InitiatedByType InitiatedBy { get; set; }
        
        /// <summary> The reason for deletion the token. </summary>
        public ReasonType Reason { get; set; }
    }
}
