using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum EntityRoles
    {
        [EnumMember(Value = "ubo")] Ubo,
        [EnumMember(Value = "legal_representative")] LegalRepresentative,
        [EnumMember(Value = "authorised_signatory")] AuthorisedSignatory,
        [EnumMember(Value = "control_person")] ControlPerson,
    }
}