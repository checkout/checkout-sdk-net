using System;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// Not defined by any Accounts API schema: <c>financial_details</c> carries the three amounts and
    /// the currency only. Retained so existing code keeps compiling.
    /// </summary>
    [Obsolete("Not defined by any Accounts API schema. Will be removed in a future major version.")]
    public class FinancialDocuments
    {
        /// <summary>
        /// Not defined by any Accounts API schema.
        /// </summary>
        public EntityDocument BankStatement { get; set; }

        /// <summary>
        /// Not defined by any Accounts API schema.
        /// </summary>
        public EntityDocument FinancialStatement { get; set; }
    }
}