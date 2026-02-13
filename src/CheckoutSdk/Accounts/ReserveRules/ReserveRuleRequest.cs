using System;

namespace Checkout.Accounts.ReserveRules
{
    public class ReserveRuleRequest
    {
        public string Type { get; set; }

        public Rolling Rolling { get; set; }

        public DateTime? ValidFrom { get; set; }
    }
}