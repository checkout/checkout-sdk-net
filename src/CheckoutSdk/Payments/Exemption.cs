using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum Exemption
    {
        [EnumMember(Value = "low_value")] LowValue,

        [EnumMember(Value = "secure_corporate_payment")]
        SecureCorporatePayment,

        [EnumMember(Value = "trusted_listing")]
        TrustedListing,

        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment
    }
}