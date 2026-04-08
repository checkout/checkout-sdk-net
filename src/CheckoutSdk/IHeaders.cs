namespace Checkout
{
    /// <summary>
    /// Marker interface for custom HTTP headers.
    /// Implementations define headers as properties decorated with <c>[JsonProperty]</c>.
    /// The <see cref="ApiClient"/> resolves header names and values via reflection.
    /// </summary>
    public interface IHeaders
    {
    }
}
