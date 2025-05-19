namespace Checkout.Forward.Requests.Sources
{
    public abstract class AbstractSource
    {
        protected AbstractSource(SourceType type) { Type = type; }

        /// <summary> The payment source type (Required) </summary>
        public SourceType? Type { get; set; }
    }
}