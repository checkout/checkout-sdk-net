using System.Runtime.Serialization;

namespace Checkout.Forward.Requests.Signatures
{
    public enum SignatureType
    {
        [EnumMember(Value = "dlocal")]
        Dlocal,
    }
}