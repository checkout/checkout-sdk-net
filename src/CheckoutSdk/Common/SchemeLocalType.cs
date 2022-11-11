using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum SchemeLocalType
    {
        [EnumMember(Value = "Cartes Bancaires")] CartesBancaires,
        [EnumMember(Value = "Mada")] Mada,
        [EnumMember(Value = "Omannet")] Omannet,
    }
}