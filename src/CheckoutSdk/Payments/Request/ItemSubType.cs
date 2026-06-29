using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    /// <summary>
    /// The digital item sub-type. Required if ItemType is Digital.
    /// </summary>
    public enum ItemSubType
    {
        /// <summary>Blockchain-based digital item.</summary>
        [EnumMember(Value = "blockchain")]
        Blockchain,

        /// <summary>Central bank digital currency (CBDC).</summary>
        [EnumMember(Value = "cbdc")]
        Cbdc,

        /// <summary>Cryptocurrency.</summary>
        [EnumMember(Value = "cryptocurrency")]
        Cryptocurrency,

        /// <summary>Non-fungible token (NFT).</summary>
        [EnumMember(Value = "nft")]
        Nft,

        /// <summary>Stablecoin.</summary>
        [EnumMember(Value = "stablecoin")]
        Stablecoin,
    }
}