namespace Checkout.NetworkTokens.PostNetworkTokens.Requests.Sources
{
    public abstract class AbstractSource
    {
        /// <summary> The source type </summary>
        public SourceType? Type { get; set; }
        protected AbstractSource(SourceType type) { Type = type; }
    }
}
