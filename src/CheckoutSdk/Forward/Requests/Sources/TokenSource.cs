namespace Checkout.Forward.Requests.Sources
{
    public class TokenSource : AbstractSource
    {
        /// <summary>Initializes a new instance of the TokenSource class.</summary>
        public TokenSource() : base(SourceType.Token) { }

        /// <summary> The unique Checkout.com token (Required, pattern ^(tok)_(\w{26})$) </summary>
        public string Token { get; set; }
    }
}