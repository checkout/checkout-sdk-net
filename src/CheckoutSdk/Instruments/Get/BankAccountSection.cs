using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// A group of bank account fields to display together.
    /// </summary>
    public class BankAccountSection
    {
        /// <summary>
        /// The name of the section.
        /// [Required]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The fields belonging to the section.
        /// [Optional]
        /// </summary>
        public IList<BankAccountField> Fields { get; set; }
    }
}
