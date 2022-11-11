using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum SchemeLocalType
    {
        [EnumMember(Value = "cartes_bancaires")] CartesBancaires,
        [EnumMember(Value = "mada")] Mada,
        [EnumMember(Value = "omannet")] Omannet,
    }
}