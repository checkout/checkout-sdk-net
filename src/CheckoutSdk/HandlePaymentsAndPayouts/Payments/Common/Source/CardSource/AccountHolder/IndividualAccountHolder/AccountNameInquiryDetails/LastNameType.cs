using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder.IndividualAccountHolder.AccountNameInquiryDetails
{
    public enum LastNameType
    {
        [EnumMember(Value = "full_match")]
        FullMatch,

        [EnumMember(Value = "partial_match")]
        PartialMatch,

        [EnumMember(Value = "no_match")]
        NoMatch,
    }
}