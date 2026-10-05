using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// The sub-entity's company details. One class covers the <c>company</c> object of every
    /// Accounts API variant that has one (the v2.0 sole trader variants use <c>individual</c>
    /// instead) and the controlling company of a v3.0 representative (see
    /// <see cref="Representative.Company"/>). Each property says which variants define it.
    /// </summary>
    public class Company
    {
        // Common

        /// <summary>
        /// The legal name of the sub-entity.
        /// [Required] for every company variant (EEA, GB and US Company Full (3.0), US ISV Seller
        /// Company (3.0), and EEA, GB and US Company Full and Lite (2.0)) and for a controlling
        /// company representative; not part of the sole trader variants.
        /// min 2 characters, max 300 characters
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        /// The trading name of the sub-entity, also referred to as 'Doing Business As'. For sole
        /// traders, the trading name the sole trader uses.
        /// [Required] for every variant that has a company object, sole traders included, and for a
        /// controlling company representative.
        /// min 2 characters, max 300 characters
        /// </summary>
        public string TradingName { get; set; }

        /// <summary>
        /// The collection of additional trading names for the sub-entity. Each item is one
        /// additional trading name.
        /// [Optional] (US ISV Seller Company (3.0) and US ISV Seller Sole Trader (3.0) only)
        /// </summary>
        public IList<string> AdditionalTradingNames { get; set; }

        /// <summary>
        /// Indicates whether the sub-entity is a registered legal entity. Must be <c>false</c>.
        /// [Required] for US ISV Seller Sole Trader (3.0), the only variant that defines it.
        /// Enum: false
        /// </summary>
        public bool? IsRegisteredCompany { get; set; }

        /// <summary>
        /// The sub-entity's Business Registration Number. This can be a Commercial Registration or
        /// Ministry of Commerce certificate number, or any other equivalent registration number.
        /// For US entities, this is the Employer Identification Number (EIN).
        /// [Required] for EEA, GB and US Company Full (3.0), US ISV Seller Company (3.0) and EEA, GB
        /// and US Company Full (2.0); [Optional] for EEA, GB and US Company Lite (2.0); not part of
        /// the sole trader variants.
        /// Length and pattern depend on the variant:
        /// <list type="bullet">
        /// <item><description>EEA Company Full (3.0) and EEA Company Full and Lite (2.0): min 2
        /// characters, max 39 characters</description></item>
        /// <item><description>GB Company Full (3.0): exactly 8 characters,
        /// ^(((AC|CE|CS|FC|FE|GE|GS|IC|LP|NC|NF|NI|NL|NO|NP|OC|OE|PC|R0|RC|SA|SC|SE|SF|SG|SI|SL|SO|SR|SZ|ZC|\d{2})\d{6})|((IP|SP|RS)[A-Z\d]{6})|(SL\d{5}[\dA]))$</description></item>
        /// <item><description>GB Company Full and Lite (2.0): exactly 8 characters,
        /// ^(([Aa][Cc]|[Cc][Ee]|[Ee][Nn]|[Ee][Ss]|[Ff][Cc]|[Gg][Ee]|[Gg][Nn]|[Gg][Ss]|[Ii][Cc]|[Ii][Pp]|[Ll][Pp]|[Nn][Aa]|[Nn][Cc]|[Nn][Ff]|[Nn][Ii]|[Nn][Ll]|[Nn][Oo]|[Nn][Pp]|[Nn][Rr]|[Nn][Vv]|[Nn][Zz]|[Oo][Cc]|[Rr][0]|[Rr][Cc]|[Ss][Aa]|[Ss][Cc]|[Ss][Ee]|[Ss][Ff]|[Ss][Ii]|[Ss][Ll]|[Ss][Oo]|[Ss][Pp]|[Ss][Rr]|[Ss][Zz]|[Zz][Cc]|\d{2})\d{6})$</description></item>
        /// <item><description>US Company Full (3.0) and US Company Full and Lite (2.0): exactly 9
        /// characters, ^[0-9]{9}$</description></item>
        /// <item><description>US ISV Seller Company (3.0): US EIN, nine digits with an optional
        /// hyphen, min 9 characters, max 11 characters, ^[0-9]{2}-?[0-9]{7}$</description></item>
        /// </list>
        /// </summary>
        public string BusinessRegistrationNumber { get; set; }

        /// <summary>
        /// The date the company was incorporated. For sole traders, the date the sole trader started
        /// trading.
        /// [Required] for every v3.0 variant (EEA, GB and US Company and Sole Trader Full, US ISV
        /// Seller Company and Sole Trader); [Optional] for EEA, GB and US Company Full (2.0); not part
        /// of the Company Lite (2.0) variants.
        /// </summary>
        public DateOfIncorporation DateOfIncorporation { get; set; }

        /// <summary>
        /// The primary location of the company where business is performed.
        /// [Required] for every variant that has a company object, sole traders included; not part of
        /// a controlling company representative.
        /// </summary>
        public Address PrincipalAddress { get; set; }

        /// <summary>
        /// The registered address of the company.
        /// [Required] for every company variant (EEA, GB and US Company Full (3.0), US ISV Seller
        /// Company (3.0), and EEA, GB and US Company Full and Lite (2.0)) and for a controlling
        /// company representative; not part of the sole trader variants.
        /// </summary>
        public Address RegisteredAddress { get; set; }

        /// <summary>
        /// The list of the company's representatives. Sole traders must have exactly one
        /// representative, the individual themselves, with roles set to <c>ubo</c>.
        /// [Required] for every variant that has a company object; not part of a controlling company
        /// representative.
        /// min 1 item; max 25 items on EEA, GB and US Company Full (3.0); max 5 items on EEA, GB and
        /// US Company Full and Lite (2.0); max 1 item on the sole trader variants (EEA, GB and US Sole
        /// Trader Full (3.0) and US ISV Seller Sole Trader (3.0)); no maximum on US ISV Seller Company
        /// (3.0)
        /// </summary>
        public IList<Representative> Representatives { get; set; }

        /// <summary>
        /// The legal type of the company. The allowed values differ per variant; US ISV Seller Sole
        /// Trader (3.0) accepts <c>individual_or_sole_proprietorship</c> only.
        /// [Required] for every v3.0 variant (EEA, GB and US Company and Sole Trader Full, US ISV
        /// Seller Company and Sole Trader) and EEA and US Company Full (2.0); [Optional] for EEA and
        /// US Company Lite (2.0); not part of GB Company Full and Lite (2.0).
        /// </summary>
        public BusinessType? BusinessType { get; set; }

        /// <summary>
        /// Seller financial questions and supporting documents.
        /// [Required] for EEA and US Company Full (2.0); [Optional] for EEA and US Company Lite (2.0);
        /// not part of the other variants.
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
        /// Not defined by any Accounts API company schema; the API does not read it. Company documents
        /// go on the top-level request documents instead. Retained so existing code keeps compiling.
        /// </summary>
        [Obsolete("Not defined by any Accounts API company schema. Will be removed in a future major version.")]
        public EntityDocument Document { get; set; }

        /// <summary>
        /// The regulatory licence number of the company.
        /// [Optional] (EEA Company Full (3.0) only)
        /// ^[a-zA-Z0-9\-]+$
        /// min 4 characters, max 32 characters
        /// </summary>
        public string RegulatoryLicenceNumber { get; set; }
    }
}
