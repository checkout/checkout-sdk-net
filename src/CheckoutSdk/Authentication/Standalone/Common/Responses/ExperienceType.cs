using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum ExperienceType
    {
        [EnumMember(Value = "3ds")]
        Threeds,

        [EnumMember(Value = "google_spa")]
        GoogleSpa,
    }
}