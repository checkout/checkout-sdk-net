using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Company
{
    public enum EntityRoles
    {
        [EnumMember(Value = "ubo")]
        Ubo,
        
        [EnumMember(Value = "authorised_signatory")]
        AuthorisedSignatory,
        
        [EnumMember(Value = "director")]
        Director,
        
        [EnumMember(Value = "control_person")]
        ControlPerson,
        
        [EnumMember(Value = "legal_representative")]
        LegalRepresentative
    }
}