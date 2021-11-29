using System.Runtime.Serialization;

namespace Checkout.Webhooks
{
    public enum WebhookContentType
    {
        [EnumMember(Value = "json")] Json,
        [EnumMember(Value = "xml")] Xml,
    }
}