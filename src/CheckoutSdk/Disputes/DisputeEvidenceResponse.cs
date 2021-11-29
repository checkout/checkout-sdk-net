using System;

namespace Checkout.Disputes
{
    public sealed class DisputeEvidenceResponse : IEquatable<DisputeEvidenceResponse>
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

        public bool Equals(DisputeEvidenceResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProofOfDeliveryOrServiceFile == other.ProofOfDeliveryOrServiceFile &&
                   ProofOfDeliveryOrServiceText == other.ProofOfDeliveryOrServiceText &&
                   InvoiceOrReceiptFile == other.InvoiceOrReceiptFile &&
                   InvoiceOrReceiptText == other.InvoiceOrReceiptText &&
                   InvoiceShowingDistinctTransactionsFile == other.InvoiceShowingDistinctTransactionsFile &&
                   InvoiceShowingDistinctTransactionsText == other.InvoiceShowingDistinctTransactionsText &&
                   CustomerCommunicationFile == other.CustomerCommunicationFile &&
                   CustomerCommunicationText == other.CustomerCommunicationText &&
                   RefundOrCancellationPolicyFile == other.RefundOrCancellationPolicyFile &&
                   RefundOrCancellationPolicyText == other.RefundOrCancellationPolicyText &&
                   RecurringTransactionAgreementFile == other.RecurringTransactionAgreementFile &&
                   RecurringTransactionAgreementText == other.RecurringTransactionAgreementText &&
                   AdditionalEvidenceFile == other.AdditionalEvidenceFile &&
                   AdditionalEvidenceText == other.AdditionalEvidenceText &&
                   ProofOfDeliveryOrServiceDateFile == other.ProofOfDeliveryOrServiceDateFile &&
                   ProofOfDeliveryOrServiceDateText == other.ProofOfDeliveryOrServiceDateText;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is DisputeEvidenceResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ProofOfDeliveryOrServiceFile);
            hashCode.Add(ProofOfDeliveryOrServiceText);
            hashCode.Add(InvoiceOrReceiptFile);
            hashCode.Add(InvoiceOrReceiptText);
            hashCode.Add(InvoiceShowingDistinctTransactionsFile);
            hashCode.Add(InvoiceShowingDistinctTransactionsText);
            hashCode.Add(CustomerCommunicationFile);
            hashCode.Add(CustomerCommunicationText);
            hashCode.Add(RefundOrCancellationPolicyFile);
            hashCode.Add(RefundOrCancellationPolicyText);
            hashCode.Add(RecurringTransactionAgreementFile);
            hashCode.Add(RecurringTransactionAgreementText);
            hashCode.Add(AdditionalEvidenceFile);
            hashCode.Add(AdditionalEvidenceText);
            hashCode.Add(ProofOfDeliveryOrServiceDateFile);
            hashCode.Add(ProofOfDeliveryOrServiceDateText);
            return hashCode.ToHashCode();
        }
    }
}