using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The bank account field formatting requirements returned by
    /// GET /validation/bank-accounts/{country}/{currency}.
    /// </summary>
    public class BankAccountFieldResponse : HttpMetadata
    {
        /// <summary>
        /// The sections of fields to display.
        /// [Optional]
        /// </summary>
        public IList<BankAccountSection> Sections { get; set; }
    }
}
