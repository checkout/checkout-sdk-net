using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum CaptureType
    {
        [EnumMember(Value = "NonFinal")] NonFinal,
        [EnumMember(Value = "Final")] Final,
    }
}