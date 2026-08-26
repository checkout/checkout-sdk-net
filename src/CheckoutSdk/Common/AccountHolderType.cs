using System.Runtime.Serialization;

namespace Checkout.Common
{
    /// <summary>
    /// The type of the legal account holder.
    /// </summary>
    public enum AccountHolderType
    {
        [EnumMember(Value = "individual")]
        Individual,

        [EnumMember(Value = "corporate")]
        Corporate,

        /// <summary>
        /// Not declared by the AccountHolder schema or the account-holder-type query parameter,
        /// which allow individual, corporate and government only. Retained for backwards
        /// compatibility.
        /// </summary>
        [EnumMember(Value = "instrument")]
        Instrument,

        [EnumMember(Value = "government")]
        Government
    }
}
