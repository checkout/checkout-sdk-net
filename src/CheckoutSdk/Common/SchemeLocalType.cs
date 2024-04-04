using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum SchemeLocalType
    {
        [EnumMember(Value = "accel")] Accel,
        [EnumMember(Value = "cartes_bancaires")] CartesBancaires,
        [EnumMember(Value = "mada")] Mada,
        [EnumMember(Value = "nyce")] Nyce,
        [EnumMember(Value = "omannet")] Omannet,
        [EnumMember(Value = "pulse")] Pulse,
        [EnumMember(Value = "star")] Star,
        [EnumMember(Value = "upi")] Upi,
    }
}