using System.Runtime.Serialization;

namespace Checkout.Disputes
{
    public enum DisputeRelevantEvidence
    {
        [EnumMember(Value = "proof_of_delivery_or_service")]
        ProofOfDeliveryOrService,

        [EnumMember(Value = "invoice_or_receipt")]
        InvoiceOrReceipt,

        [EnumMember(Value = "invoice_showing_distinct_transactions")]
        InvoiceShowingDistinctTransactions,

        [EnumMember(Value = "customer_communication")]
        CustomerCommunication,

        [EnumMember(Value = "refund_or_cancellation_policy")]
        RefundOrCancellationPolicy,

        [EnumMember(Value = "recurring_transaction_agreement")]
        RecurringTransactionAgreement,

        [EnumMember(Value = "additional_evidence")]
        AdditionalEvidence,
        
        [EnumMember(Value = "arbitration_no_review")]
        ArbitrationNoReview,
        
        [EnumMember(Value = "arbitration_review_required")]
        ArbitrationReviewRequired
    }
}