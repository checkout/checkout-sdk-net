using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum EntityRoles
    {
        [EnumMember(Value = "ubo")] Ubo,
        [EnumMember(Value = "legal_representative")] LegalRepresentative,
    }
}