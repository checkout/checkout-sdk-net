﻿using Checkout.Accounts.Regional;
using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Accounts
{
    public class Company
    {
        public string BusinessRegistrationNumber { get; set; }
        
        public BusinessType? BusinessType { get; set; }

        public string LegalName { get; set; }

        public string TradingName { get; set; }

        public Address PrincipalAddress { get; set; }

        public Address RegisteredAddress { get; set; }

        public IList<Representative> Representatives { get; set; }

        public EntityDocument Document { get; set; }
        
        public EntityFinancialDetails FinancialDetails { get; set; }
    }
}