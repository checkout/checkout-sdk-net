using Newtonsoft.Json;

namespace Checkout.Sessions.Completion
{
    public abstract class CompletionInfo
    {
        [JsonProperty(PropertyName = "type")]
        protected readonly CompletionInfoType Type;

        protected CompletionInfo(CompletionInfoType type)
        {
            Type = type;
        }
    }
}