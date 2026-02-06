namespace Checkout.Identities.Common
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