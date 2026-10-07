using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.ContactDetails;
using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    /// <summary>
    /// The details of a sub-entity, as returned by GET /accounts/entities/{id}.
    /// </summary>
    public class OnboardEntityDetailsResponse : Resource
    {
        /// <summary>
        /// The ID of the sub-entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A unique reference you can later use to identify the sub-entity.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The capabilities of the entity.
        /// </summary>
        public Capabilities Capabilities { get; set; }

        /// <summary>
        /// The onboarding status of the sub-entity.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// List of requirements due in order to be onboarded.
        /// </summary>
        public IList<RequirementsDue> RequirementsDue { get; set; }

        /// <summary>
        /// Contact details of this sub-entity.
        /// </summary>
        public ContactDetails ContactDetails { get; set; }

        /// <summary>
        /// Information about the profile of the sub-entity, primarily regarding the products and
        /// services offered.
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Information about the company represented by the sub-entity (company and v3.0 sole trader
        /// variants).
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Information about the individual represented by the sub-entity (v2.0 sole trader variants).
        /// </summary>
        public Individual Individual { get; set; }

        /// <summary>
        /// The sub-entity's expected processing (Accounts API v3.0). Amounts are <c>long</c>; see
        /// <see cref="EntityProcessingDetails"/>.
        /// </summary>
        public EntityProcessingDetails ProcessingDetails { get; set; }

        /// <summary>
        /// The top-level documents used to support the verification of the sub-entity's details.
        /// Representative documents are on <see cref="Representative.Documents"/>, under
        /// <see cref="Company"/>.
        /// </summary>
        public Common.Documents.Documents Documents { get; set; }

        /// <summary>
        /// The sub-entity's payment instruments.
        /// </summary>
        public IList<Instrument> Instruments { get; set; }
    }
}