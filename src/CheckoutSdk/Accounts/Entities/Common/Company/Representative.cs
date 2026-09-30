using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// A representative of the sub-entity. One class covers every shape the Accounts API defines:
    /// <list type="bullet">
    /// <item><description>v3.0 person of interest: <see cref="Individual"/>, <see cref="Roles"/>,
    /// <see cref="CompanyPosition"/>, <see cref="OwnershipPercentage"/>, <see cref="Documents"/>.</description></item>
    /// <item><description>v3.0 controlling company (EEA and GB Company Full): <see cref="Company"/> and
    /// <see cref="OwnershipPercentage"/>.</description></item>
    /// <item><description>v2.0 company representatives: the flat person fields, <see cref="Roles"/>,
    /// <see cref="Documents"/> and, on the US variants, <see cref="Identification"/>.</description></item>
    /// </list>
    /// </summary>
    public class Representative
    {
        // Common

        /// <summary>
        /// The representative's id.
        /// [Optional]
        /// ^rep_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The percentage ownership of the UBO or controlling company (required when over 25%).
        /// [Optional]
        /// min 25, max 100 on the EEA, GB and US Company Full (3.0) variants; min 0, max 100 on the
        /// US ISV Seller variants
        /// </summary>
        public int? OwnershipPercentage { get; set; }

        /// <summary>
        /// The individual's roles within the company. For sole traders, must be <c>ubo</c> only.
        /// [Required] for every variant except EEA and US Company Lite (2.0), where it is [Optional].
        /// </summary>
        public IList<EntityRoles> Roles { get; set; }

        /// <summary>
        /// Verification documents for the individual representative. The API validates this object
        /// strictly on v3.0: it accepts only <c>identity_verification</c>,
        /// <c>certified_authorised_signatory</c>, <c>proof_of_residential_address</c> and
        /// <c>proof_of_registration</c>, and rejects any other key. See
        /// <see cref="Documents.Documents"/> for which apply to each variant.
        /// [Required] for the EEA, GB and US Sole Trader Full (3.0) variants and EEA Company Full
        /// (2.0); [Optional] otherwise.
        /// </summary>
        public Documents.Documents Documents { get; set; }

        // 3.0 person of interest

        /// <summary>
        /// Information about the individual representing the sub-entity.
        /// [Required] for every v3.0 person of interest.
        /// </summary>
        public Individual Individual { get; set; }

        /// <summary>
        /// The position of the representative within the company (required for the
        /// <c>control_person</c> role).
        /// [Optional] (EEA, GB and US Company Full (3.0) and US ISV Seller Company (3.0))
        /// </summary>
        public CompanyPositionType? CompanyPosition { get; set; }

        // 3.0 controlling company

        /// <summary>
        /// The controlling company, when the representative is a company rather than an individual.
        /// [Required] for a controlling company representative (EEA and GB Company Full (3.0) only).
        /// </summary>
        public Company Company { get; set; }

        // 2.0 company representatives

        /// <summary>
        /// The representative's first name. Accounts API v2.0 only; on v3.0 use
        /// <see cref="Individual"/>.
        /// [Required] (v2.0)
        /// min 2 characters, max 50 characters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The representative's last name. Accounts API v2.0 only; on v3.0 use
        /// <see cref="Individual"/>.
        /// [Required] (v2.0)
        /// min 2 characters, max 50 characters
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The representative's address. Accounts API v2.0 only; on v3.0 use
        /// <see cref="Individual"/>.
        /// [Required] (v2.0)
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The date of birth of the person according to the Gregorian calendar. Accounts API v2.0
        /// only; on v3.0 use <see cref="Individual"/>.
        /// [Required] for the v2.0 Full variants; [Optional] for the v2.0 Lite variants.
        /// </summary>
        public DateOfBirth DateOfBirth { get; set; }

        /// <summary>
        /// The representative's middle name. Required if it appears in official documents. Accounts
        /// API v2.0 only; on v3.0 use <see cref="Individual"/>.
        /// [Optional]
        /// min 2 characters, max 50 characters
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The representative's phone number. Accounts API v2.0 only; on v3.0 use
        /// <see cref="Individual"/>.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The place of birth of the person. Accounts API v2.0 only; on v3.0 use
        /// <see cref="Individual"/>.
        /// [Required] for EEA Company Full (2.0); [Optional] for EEA Company Lite (2.0). Not part of the
        /// other v2.0 variants.
        /// </summary>
        public PlaceOfBirth PlaceOfBirth { get; set; }

        /// <summary>
        /// The representative's identification. Accounts API v2.0 US Company variants only.
        /// [Required] for US Company Full (2.0); [Optional] for US Company Lite (2.0).
        /// </summary>
        public Identification Identification { get; set; }
    }
}