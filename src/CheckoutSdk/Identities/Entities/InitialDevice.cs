using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The type of device the applicant used to start the attempt.
    /// </summary>
    public enum InitialDevice
    {
        [EnumMember(Value = "desktop")]
        Desktop,

        [EnumMember(Value = "mobile")]
        Mobile
    }
}
