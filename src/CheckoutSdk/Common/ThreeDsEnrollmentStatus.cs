using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum ThreeDsEnrollmentStatus
    {
        [EnumMember(Value = "Y")] IssuerEnrolled,
        [EnumMember(Value = "N")] CustomerNotEnrolled,
        [EnumMember(Value = "U")] Unknown,
    }
}