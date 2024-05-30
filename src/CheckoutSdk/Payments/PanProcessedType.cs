using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum PanProcessedType
    {
        [EnumMember(Value = "fpan")] FPAN,
        [EnumMember(Value = "dpan")] DPAN,
    }
}