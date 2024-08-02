using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum Exemption
    {
        [EnumMember(Value = "3ds_outage")] ThreeDsOutage,

        [EnumMember(Value = "low_risk_program")]
        LowRiskProgram,
        
        [EnumMember(Value = "low_value")] LowValue,
        [EnumMember(Value = "none")] None,
        [EnumMember(Value = "other")] Other,

        [EnumMember(Value = "out_of_sca_scope")]
        OutOfScaScope,

        [EnumMember(Value = "recurring_operation")]
        RecurringOperation,
        
        [EnumMember(Value = "sca_delegation")] ScaDelegation,

        [EnumMember(Value = "secure_corporate_payment")]
        SecureCorporatePayment,

        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment,

        [EnumMember(Value = "trusted_listing")]
        TrustedListing
    }
}