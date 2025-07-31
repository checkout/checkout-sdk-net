using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.InitialTransaction
{
    public enum AuthenticationMethodType
    {
        [EnumMember(Value = "frictionless_authentication")]
        FrictionlessAuthentication,

        [EnumMember(Value = "challenge_occurred")]
        ChallengeOccurred,

        [EnumMember(Value = "avs_verified")]
        AvsVerified,

        [EnumMember(Value = "other_issuer_methods")]
        OtherIssuerMethods,
    }
}