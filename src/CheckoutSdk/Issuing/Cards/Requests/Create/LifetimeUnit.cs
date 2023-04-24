using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public enum LifetimeUnit
    {
        [EnumMember(Value = "months")] Months,
        [EnumMember(Value = "years")] Years
    }
}