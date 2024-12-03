using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum PreferredExperiences
    {
        [EnumMember(Value = "google_spa")]
        GoogleSpa,
        
        [EnumMember(Value = "3ds")]
        ThreeDs,
    }
}