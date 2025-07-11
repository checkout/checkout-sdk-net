using System.Runtime.Serialization;

namespace Checkout.NetworkTokens.PostCryptograms.Requests
{
    public enum TransactionType
    {
        [EnumMember(Value = "ecom")]
        Ecom,
        
        [EnumMember(Value = "recurring")]
        Recurring,
        
        [EnumMember(Value = "pos")]
        Pos,
        
        [EnumMember(Value = "aft")]
        Aft,
    }
}
