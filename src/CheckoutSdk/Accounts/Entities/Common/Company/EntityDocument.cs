using System;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// Not defined by any Accounts API onboarding schema. Referenced only by the obsolete
    /// <c>Company.Document</c> and <c>FinancialDocuments</c>. Retained so existing code keeps compiling.
    /// </summary>
    [Obsolete("Not defined by any Accounts API onboarding schema. Will be removed in a future major version.")]
    public class EntityDocument
    {
        /// <summary>
        /// Not defined by any Accounts API onboarding schema.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Not defined by any Accounts API onboarding schema.
        /// </summary>
        public string FileId { get; set; }
    }
}