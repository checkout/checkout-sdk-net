namespace Checkout.Identities.Entities
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