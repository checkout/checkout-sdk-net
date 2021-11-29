using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum ThreeDsEnrollmentStatus
    {
        [EnumMember(Value = "Y")] Yes,
        [EnumMember(Value = "N")] No,
        [EnumMember(Value = "U")] U,
    }
}