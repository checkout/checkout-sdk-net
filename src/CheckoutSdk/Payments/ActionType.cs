using System.Runtime.Serialization;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the type of payment action.
    /// </summary>
    public static class ActionType
    {
        public const string Authorization = "Authorization";
        public const string CardVerification = "Card Verification";
        public const string Void = "Void";
        public const string Capture = "Capture";
        public const string Refund = "Refund";
    }
}
