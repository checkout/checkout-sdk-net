using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest
{
    public enum PreferredExperiencesType
    {
        [EnumMember(Value = "3ds")]
        Threeds,

        [EnumMember(Value = "google_spa")]
        GoogleSpa,
    }
}