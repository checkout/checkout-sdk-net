using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The card account holder type.
    /// </summary>
    public enum PaymentSetupCardAccountHolderType
    {
        [EnumMember(Value = "individual")]
        Individual,

        [EnumMember(Value = "government")]
        Government,

        [EnumMember(Value = "corporate")]
        Corporate
    }
}
