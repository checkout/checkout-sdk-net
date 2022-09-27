using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum BillingPlanType
    {
        [EnumMember(Value = "MERCHANT_INITIATED_BILLING")] MerchantInitiatedBilling,
        [EnumMember(Value = "MERCHANT_INITIATED_BILLING_SINGLE_AGREEMENT")] MerchantInitiatedBillingSingleAgreement,
        [EnumMember(Value = "CHANNEL_INITIATED_BILLING")] ChannelInitiatedBilling,
        [EnumMember(Value = "CHANNEL_INITIATED_BILLING_SINGLE_AGREEMENT")] ChannelInitiatedBillingSingleAgreement,
        [EnumMember(Value = "RECURRING_PAYMENTS")] RecurringPayments,
        [EnumMember(Value = "PRE_APPROVED_PAYMENTS")] PreApprovedPayments
    }
}