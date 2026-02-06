namespace Checkout.Identities.Common
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