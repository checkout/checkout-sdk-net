using System;
using Checkout.Common;

namespace Checkout.Accounts.ReserveRules
{
    public class ReserveRuleResponse : ReserveRuleResponseBase
    {
        /// <summary>
        /// The reserve rule Id
        /// [Required]
        /// </summary>
        public string Id { get; set; }
    }
}