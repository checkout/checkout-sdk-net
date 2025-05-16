using System.Runtime.Serialization;

namespace Checkout.Issuing.Cardholders.Requests
{
    public enum CardholderType
    {
        [EnumMember(Value = "individual")] Individual
    }
}