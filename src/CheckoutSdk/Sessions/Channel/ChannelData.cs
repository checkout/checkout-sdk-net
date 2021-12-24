using Newtonsoft.Json;

namespace Checkout.Sessions.Channel
{
    public abstract class ChannelData
    {
        [JsonProperty(PropertyName = "channel")]
        protected readonly ChannelType _channel;

        protected ChannelData(ChannelType channel)
        {
            _channel = channel;
        }
    }
}