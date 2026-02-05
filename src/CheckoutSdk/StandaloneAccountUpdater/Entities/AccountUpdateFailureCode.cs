using System.Runtime.Serialization;

namespace Checkout.StandaloneAccountUpdater.Entities
{
    public enum AccountUpdateFailureCode
    {
        [EnumMember(Value = "CARDHOLDER_OPT_OUT")] CardholderOptOut,
        [EnumMember(Value = "UP_TO_DATE")] UpToDate,
        [EnumMember(Value = "NON_PARTICIPATING_BIN")] NonParticipatingBin,
        [EnumMember(Value = "UNKNOWN")] Unknown
    }
}