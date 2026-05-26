using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Accounts.Entities.Requirements
{
    /// <summary>
    /// The list of pending requirements for a sub-entity.
    /// </summary>
    public class EntityRequirementListResponse : Resource
    {
        /// <summary>
        /// The list of pending requirements for the sub-entity. Empty when no requirements are outstanding.
        /// [Optional]
        /// </summary>
        public IList<EntityRequirementListItem> Data { get; set; }
    }
}
