using System.Runtime.Serialization;

namespace Checkout.Sources
{
    public enum SourceType
    {
        [EnumMember(Value = "sepa")] Sepa
    }
}