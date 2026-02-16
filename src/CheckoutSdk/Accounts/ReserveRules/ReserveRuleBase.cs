using System;

namespace Checkout.Accounts.ReserveRules
{
    /// <summary>
    /// Base class containing common properties for Reserve Rule operations
    /// </summary>
    public abstract class ReserveRuleBase
    {
        /// <summary>
        /// The type of reserve rule. For example, rolling.
        /// [Required]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The rolling reserve rule details.   
        /// [Required]
        /// </summary>
        public RollingReserveRule Rolling { get; set; }

        /// <summary>
        /// The date and time the reserve rule will come into effect.
        /// This must be at least 15 minutes in the future.
        /// [Required]
        /// </summary>
        public DateTime? ValidFrom { get; set; }
    }
}