namespace Checkout.NetworkTokens.PostNetworkTokens.Requests.Sources
{
    public class IdSource : AbstractSource
    {
        /// <summary> Initializes a new instance of the IdSource class. </summary>
        public IdSource() : base(SourceType.Id) { }

        /// <summary> The card instrument (Required, constraints: ^(src)_(\w{26})$) </summary>
        public string Id { get; set; }
    }
}
