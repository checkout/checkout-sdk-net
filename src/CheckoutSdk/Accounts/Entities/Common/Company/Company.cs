using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Common.Company
{
    public class Company
    {
        // Common
        public string LegalName { get; set; }

        public string TradingName { get; set; }

        public string BusinessRegistrationNumber { get; set; }

        public DateOfIncorporation DateOfIncorporation { get; set; }
        
        public Address PrincipalAddress { get; set; }

        public Address RegisteredAddress { get; set; }

        public IList<Representative> Representatives { get; set; }
        
        public BusinessType? BusinessType { get; set; }
        
        public FinancialDetails FinancialDetails { get; set; }
        
        // EEA Company Full (3.0) Company
        
        public string RegulatoryLicenseNumber { get; set; }
        
        // Unknown
        public EntityDocument Document { get; set; }
        
        public string RegulatoryLicenceNumber { get; set; }
    }
}