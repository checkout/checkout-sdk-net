using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum WalletType
    {
        [EnumMember(Value = "googlepay")]
        Googlepay,

        [EnumMember(Value = "applepay")] 
        Applepay,

        [EnumMember(Value = "remote_commerce_programs")]
        RemoteCommercePrograms,
    }
}