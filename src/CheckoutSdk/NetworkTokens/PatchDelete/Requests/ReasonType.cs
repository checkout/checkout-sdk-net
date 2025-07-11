using System.Runtime.Serialization;

namespace Checkout.NetworkTokens.PatchDelete.Requests
{
    public enum ReasonType
    {
        [EnumMember(Value = "fraud")]
        Fraud,
        
        [EnumMember(Value = "other")]
        Other,
    }
}