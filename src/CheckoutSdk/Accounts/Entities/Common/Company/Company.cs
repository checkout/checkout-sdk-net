using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Common.Company
{
    public class Company
    {
        // Common

        /// <summary>
        /// The legal name of the sub-entity.
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        /// The trading name of the sub-entity, also referred to as 'Doing Business As'.
        /// </summary>
        public string TradingName { get; set; }

        /// <summary>
        /// The collection of additional trading names for the sub-entity.
        /// </summary>
        public IList<string> AdditionalTradingNames { get; set; }

        /// <summary>
        /// Indicates whether the sub-entity is a registered legal entity. Must be <c>false</c> for
        /// sole trader variants.
        /// </summary>
        public bool? IsRegisteredCompany { get; set; }

        /// <summary>
        /// The sub-entity's Business Registration Number. This can be a Commercial Registration or
        /// Ministry of Commerce certificate number, or any other equivalent registration number.
        /// For US entities, this is the Employer Identification Number (EIN).
        /// </summary>
        public string BusinessRegistrationNumber { get; set; }

        /// <summary>
        /// The date the company was incorporated.
        /// </summary>
        public DateOfIncorporation DateOfIncorporation { get; set; }

        /// <summary>
        /// The primary location of the company where business is performed.
        /// </summary>
        public Address PrincipalAddress { get; set; }

        /// <summary>
        /// The registered address of the company.
        /// </summary>
        public Address RegisteredAddress { get; set; }

        /// <summary>
        /// The list of the company's representatives.
        /// </summary>
        public IList<Representative> Representatives { get; set; }

        /// <summary>
        /// The legal type of the company.
        /// </summary>
        public BusinessType? BusinessType { get; set; }

        /// <summary>
        /// Seller financial questions and supporting documents.
        /// </summary>
        public FinancialDetails FinancialDetails { get; set; }

        // EEA Company Full (3.0) Company

        /// <summary>
        /// The regulatory licence number of the company.
        /// Note: this property serializes to <c>regulatory_license_number</c> (US spelling), which is
        /// not part of the API schema. Prefer <see cref="RegulatoryLicenceNumber"/>, which maps to the
        /// canonical <c>regulatory_licence_number</c> field.
        /// </summary>
        public string RegulatoryLicenseNumber { get; set; }

        // Unknown

        /// <summary>
        /// A legal document used to verify the company.
        /// </summary>
        public EntityDocument Document { get; set; }

        /// <summary>
        /// The regulatory licence number of the company.
        /// </summary>
        public string RegulatoryLicenceNumber { get; set; }
    }
}
