namespace Checkout.Identities.Entities
{
    public enum AttemptStatus
    {
        CaptureAborted,
        CaptureInProgress,
        ChecksInconclusive,
        ChecksInProgress,
        Completed,
        Expired,
        PendingRedirection,
        CaptureRefused
    }
}