namespace Checkout.Payments
{
    
    /// <summary>
    /// Defines the type of payment action.
    /// </summary>
    public enum ActionType
    {
        Authorization,
        CardVerification,
        Void,
        Capture,
        Refund
    }
}
