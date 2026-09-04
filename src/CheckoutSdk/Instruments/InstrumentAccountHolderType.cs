using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The type of account holder of a stored payment instrument. Shared by the Bacs Direct Debit,
    /// SEPA and ACH instrument variants, which all declare the same two values.
    /// </summary>
    public enum InstrumentAccountHolderType
    {
        [EnumMember(Value = "individual")]
        Individual,

        [EnumMember(Value = "corporate")]
        Corporate,
    }
}
