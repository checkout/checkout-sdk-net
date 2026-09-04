using System.Runtime.Serialization;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The type of account holder on a SEPA payment source.
    /// This position declares two values only, unlike Checkout.Common.AccountHolderType which also
    /// declares "government". Declared here rather than reusing
    /// Checkout.Instruments.InstrumentAccountHolderType so the payments source namespace does not
    /// depend on the instruments namespace.
    /// The values serialize lowercase. The specification declares them capitalized at this one
    /// position, but every other account-holder-type position declares them lowercase and every
    /// other Checkout.com SDK sends lowercase. Pending confirmation from the API owners.
    /// </summary>
    public enum SepaSourceAccountHolderType
    {
        [EnumMember(Value = "individual")]
        Individual,

        [EnumMember(Value = "corporate")]
        Corporate
    }
}
