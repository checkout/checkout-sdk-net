namespace Checkout.Tokens
{
    
    /// <summary>
    /// Defines a token request.
    /// </summary>
    public interface ITokenRequest
    {
        /// <summary>
        /// Gets the type of details to be tokenized.
        /// </summary>
        string Type { get; }
    }
}