using System.Runtime.Serialization;

namespace Checkout.Sources
{
    public enum MandateType
    {
        [EnumMember(Value = "single")] Single,
        [EnumMember(Value = "recurring")] Recurring
    }
}