using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The next available action for the Pay by Bank payment method (response only).
    /// </summary>
    public class PayByBankAction
    {
        /// <summary>
        /// The type of action.
        /// [Optional]
        /// Enum: "select_bank"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The list of banks available for the customer to select.
        /// [Optional]
        /// </summary>
        public IList<PayByBankBank> Banks { get; set; }
    }
}
