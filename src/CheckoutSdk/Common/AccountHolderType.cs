using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum AccountHolderType
    {
        [EnumMember(Value = "individual")] Individual,
        [EnumMember(Value = "corporate")] Corporate,
        [EnumMember(Value = "instrument")] Instrument
    }
}