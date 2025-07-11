using System.Runtime.Serialization;

namespace Checkout.NetworkTokens.PatchDelete.Requests
{
    public enum InitiatedByType
    {
        [EnumMember(Value = "cardholder")]
        Cardholder,
        
        [EnumMember(Value = "token_requestor")]
        TokenRequestor,
        
    }
}
