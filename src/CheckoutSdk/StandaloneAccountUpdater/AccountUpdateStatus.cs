using System.Runtime.Serialization;

namespace Checkout.StandaloneAccountUpdater
{
    public enum AccountUpdateStatus
    {
        [EnumMember(Value = "CARD_UPDATED")] CardUpdated,
        [EnumMember(Value = "CARD_EXPIRY_UPDATED")] CardExpiryUpdated,
        [EnumMember(Value = "CARD_CLOSED")] CardClosed,
        [EnumMember(Value = "UPDATE_FAILED")] UpdateFailed
    }
}