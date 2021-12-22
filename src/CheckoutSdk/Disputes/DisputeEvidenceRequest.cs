namespace Checkout.Disputes
{
    public sealed class DisputeEvidenceRequest
    {
        public string ProofOfDeliveryOrServiceFile { get; set; }

        public string ProofOfDeliveryOrServiceText { get; set; }

        public string InvoiceOrReceiptFile { get; set; }

        public string InvoiceOrReceiptText { get; set; }

        public string InvoiceShowingDistinctTransactionsFile { get; set; }

        public string InvoiceShowingDistinctTransactionsText { get; set; }

        public string CustomerCommunicationFile { get; set; }

        public string CustomerCommunicationText { get; set; }

        public string RefundOrCancellationPolicyFile { get; set; }

        public string RefundOrCancellationPolicyText { get; set; }

        public string RecurringTransactionAgreementFile { get; set; }

        public string RecurringTransactionAgreementText { get; set; }

        public string AdditionalEvidenceFile { get; set; }

        public string AdditionalEvidenceText { get; set; }

        public string ProofOfDeliveryOrServiceDateFile { get; set; }

        public string ProofOfDeliveryOrServiceDateText { get; set; }
    }
}