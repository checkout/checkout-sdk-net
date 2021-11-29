using System.Runtime.Serialization;

namespace Checkout.Payments.Four
{
    public enum CaptureType
    {
        [EnumMember(Value = "NonFinal")] NonFinal,
        [EnumMember(Value = "Final")] Final,
    }
}