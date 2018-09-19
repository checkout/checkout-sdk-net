namespace Checkout.Tokens
{
    public interface ITokenRequest
    {
        /// <summary>
        /// The type of details to be tokenized
        /// </summary>
        string Type { get; }
    }
}