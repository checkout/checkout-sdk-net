using System.Runtime.Serialization;

namespace Checkout.Sources.Previous
{
    public enum SourceType
    {
        [EnumMember(Value = "sepa")] Sepa
    }
}