using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum IndicatorType
    {[EnumMember(Value = "incremental_preauthorization")]
        IncrementalPreauthorization,

        [EnumMember(Value = "deferred_authorization")]
        DeferredAuthorization,

        [EnumMember(Value = "preauthorization")]
        Preauthorization,

        [EnumMember(Value = "normal_authorization")]
        NormalAuthorization,

        [EnumMember(Value = "final_authorization")]
        FinalAuthorization,

        [EnumMember(Value = "partial_reversal")]
        PartialReversal,

        [EnumMember(Value = "full_reversal")]
        FullReversal,

        [EnumMember(Value = "partial_presentment")]
        PartialPresentment,

        [EnumMember(Value = "final_presentment")]
        FinalPresentment,

        [EnumMember(Value = "unknown")]
        Unknown
    }
}