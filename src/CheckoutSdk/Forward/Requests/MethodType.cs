using System.Runtime.Serialization;

namespace Checkout.Forward.Requests
{
    public enum MethodType
    {
        [EnumMember(Value = "GET")] Get,

        [EnumMember(Value = "POST")] Post,

        [EnumMember(Value = "PUT")] Put,

        [EnumMember(Value = "DELETE")] Delete,

        [EnumMember(Value = "PATCH")] Patch,

        [EnumMember(Value = "HEAD")] Head,

        [EnumMember(Value = "OPTIONS")] Options,

        [EnumMember(Value = "TRACE")] Trace
    }
}