using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.Exemption
{
    public enum AppliedType
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "low_value")]
        LowValue,

        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment,

        [EnumMember(Value = "secure_corporate_payment")]
        SecureCorporatePayment,

        [EnumMember(Value = "trusted_listing")]
        TrustedListing,

        [EnumMember(Value = "3ds_outage")]
        ThreedsOutage,

        [EnumMember(Value = "sca_delegation")]
        ScaDelegation,

        [EnumMember(Value = "out_of_sca_scope")]
        OutOfScaScope,

        [EnumMember(Value = "other")]
        Other,

        [EnumMember(Value = "low_risk_program")]
        LowRiskProgram,
    }
}