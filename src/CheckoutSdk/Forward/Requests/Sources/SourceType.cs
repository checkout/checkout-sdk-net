using System.Runtime.Serialization;

namespace Checkout.Forward.Requests.Sources
{
    public enum SourceType
    {
        [EnumMember(Value = "id")] Id,

        [EnumMember(Value = "token")] Token
    }
}