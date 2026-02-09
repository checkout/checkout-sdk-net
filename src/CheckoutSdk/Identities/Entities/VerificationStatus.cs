namespace Checkout.Identities.Entities
{
    public enum VerificationStatus
    {
        Approved,
        CaptureInProgress,
        ChecksInProgress,
        Created,
        Declined,
        Inconclusive,
        Pending,
        Refused,
        RetryRequired
    }
}