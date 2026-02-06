namespace Checkout.Identities.Common
{
    public enum IdDocumentVerificationStatus
    {
        Created,
        QualityChecksInProgress,
        ChecksInProgress,
        Approved,
        Declined,
        RetryRequired,
        Inconclusive
    }
}