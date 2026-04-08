using System.Runtime.Serialization;

namespace Checkout.AgenticCommerce.Entities
{
    /// <summary>
    /// The funding type of the card, used for display purposes.
    /// </summary>
    public enum DelegatedCardFundingType
    {
        [EnumMember(Value = "credit")]
        Credit,
        [EnumMember(Value = "debit")]
        Debit,
        [EnumMember(Value = "prepaid")]
        Prepaid,
    }
}
