using System.Runtime.Serialization;

namespace Checkout.Webhooks.Previous
{
    public enum WebhookContentType
    {
        [EnumMember(Value = "json")] Json
    }
}