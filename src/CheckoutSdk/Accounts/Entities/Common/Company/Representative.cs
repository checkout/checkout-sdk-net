using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Common.Company
{
    public class Representative
    {
        // Common
        
        public string Id { get; set; }
        
        public int? OwnershipPercentage { get; set; }
        
        public IList<EntityRoles> Roles { get; set; }
        
        public Documents.Documents Documents { get; set; }
        
        // 3.0 /1
        
        public Individual Individual { get; set; }
        
        
        public CompanyPositionType? CompanyPosition { get; set; }
        
        
        // 3.0 /2
        
        public Company Company { get; set; }
        
        // 2.0 Common
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Address Address { get; set; }
        public DateOfBirth DateOfBirth { get; set; }

        public string MiddleName { get; set; }
        
        public Phone Phone { get; set; }
        
        // 2.0 EEA Company Full
        
        public PlaceOfBirth PlaceOfBirth { get; set; }
    }
}