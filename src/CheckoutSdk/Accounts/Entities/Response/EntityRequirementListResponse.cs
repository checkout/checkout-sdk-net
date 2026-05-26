using System.Collections.Generic;
using Checkout.Common;

using Checkout.Accounts.Entities.Common.Requirements;

namespace Checkout.Accounts.Entities.Response
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
