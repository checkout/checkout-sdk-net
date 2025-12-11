using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    public enum ItemSubType
    {
        [EnumMember(Value = "blockchain")]
        Blockchain,
            
        [EnumMember(Value = "cbdc")]
        Cbdc,
        
        [EnumMember(Value = "cryptocurrency")]
        Cryptocurrency,
        
        [EnumMember(Value = "nft")]
        Nft,
        
        [EnumMember(Value = "stablecoin")]
        Stablecoin,
            
    }
}