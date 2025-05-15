using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public enum ReturnCredentialsType
    {
        [EnumMember(Value = "number")]
        Number,
        
        [EnumMember(Value = "cvc2")]
        Cvc2,
    }
}