using Checkout.Common;
using System;

namespace Checkout.Accounts.ReserveRules
{
    public class ReserveRuleResponse : Resource
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public Rolling Rolling { get; set; }

        public DateTime? ValidFrom { get; set; }
    }
}