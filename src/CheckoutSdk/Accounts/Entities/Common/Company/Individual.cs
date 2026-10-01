using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// An individual on the Accounts API. One class covers two objects:
    /// <list type="bullet">
    /// <item><description>v3.0: <c>company.representatives[].individual</c>, the personal details of a
    /// representative (<see cref="Representative.Individual"/>).</description></item>
    /// <item><description>v2.0: the top-level <c>individual</c> of the sole trader variants.</description></item>
    /// </list>
    /// Each property below says which of the two it belongs to.
    /// </summary>
    public class Individual
    {
        // Common

        /// <summary>
        /// The individual's first name.
        /// [Required]
        /// min 2 characters, max 50 characters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The individual's last name.
        /// [Required]
        /// min 2 characters, max 50 characters
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The date of birth of the person according to the Gregorian calendar.
        /// [Required], except on GB Sole Trader Lite (2.0) where it is [Optional].
        /// </summary>
        public DateOfBirth DateOfBirth { get; set; }

        /// <summary>
        /// The place of birth of the person.
        /// [Required] on every v3.0 variant and on EEA Sole Trader Full and Lite (2.0). Not part of
        /// the other v2.0 variants.
        /// </summary>
        public PlaceOfBirth PlaceOfBirth { get; set; }

        /// <summary>
        /// The representative's address. v3.0 only.
        /// [Required] (v3.0)
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The individual's middle name. Required if it appears in official documents.
        /// [Optional]
        /// min 2 characters, max 50 characters
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The list of citizenships or legal statuses for the representative. v3.0 only.
        /// [Required] for the US ISV Seller variants only; not part of the other v3.0 schemas, leave
        /// unset for them.
        /// </summary>
        public IList<Citizenship> Citizenships { get; set; }

        /// <summary>
        /// The classification of the national identification number provided. v3.0 only.
        /// [Required] for the US ISV Seller variants only; not part of the other v3.0 schemas, leave
        /// unset for them.
        /// </summary>
        public NationalIdType? NationalIdType { get; set; }

        /// <summary>
        /// The representative's national identification number. v3.0 only.
        /// [Required] for the US ISV Seller variants; [Optional] for the other v3.0 variants.
        /// The format depends on the variant:
        /// <list type="bullet">
        /// <item><description>US ISV Seller: the number for the <see cref="NationalIdType"/> given.
        /// ^[a-zA-Z0-9\-]+$, min 5 characters, max 16 characters.</description></item>
        /// <item><description>Other v3.0 variants: a Social Security Number (SSN) or Individual
        /// Taxpayer Identification Number (ITIN), US residents only. ^\d{9}$, 9 characters.</description></item>
        /// </list>
        /// </summary>
        public string NationalIdNumber { get; set; }

        /// <summary>
        /// The representative's personal email address. v3.0 only.
        /// [Required] for the US ISV Seller variants; [Optional] for the other v3.0 variants.
        /// Format: email
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The representative's phone number. v3.0 only.
        /// [Required] for the US ISV Seller variants; [Optional] for the other v3.0 variants.
        /// </summary>
        public Phone Phone { get; set; }

        // 2.0 sole traders

        /// <summary>
        /// Seller financial questions and supporting documents. v2.0 US Sole Trader only.
        /// [Required] for US Sole Trader Full (2.0); [Optional] for US Sole Trader Lite (2.0).
        /// </summary>
        public FinancialDetails FinancialDetails { get; set; }

        /// <summary>
        /// The individual's identification. v2.0 US Sole Trader only.
        /// [Required] for US Sole Trader Full (2.0); [Optional] for US Sole Trader Lite (2.0).
        /// </summary>
        public Identification Identification { get; set; }

        /// <summary>
        /// The registered address of the sole trader's business. v2.0 sole traders only.
        /// [Required] (v2.0 sole traders)
        /// </summary>
        public Address RegisteredAddress { get; set; }

        /// <summary>
        /// The trading name of the sub-entity, also referred to as 'doing business as'. v2.0 sole
        /// traders only.
        /// [Required] (v2.0 sole traders)
        /// min 2 characters, max 300 characters
        /// </summary>
        public string TradingName { get; set; }

        // Not defined by the API

        /// <summary>
        /// Not defined by the Accounts API for an individual: <c>legal_name</c> exists on the company
        /// only. Retained so existing code keeps compiling.
        /// </summary>
        [Obsolete("Not defined by the Accounts API for an individual; legal_name exists on the company only. Will be removed in a future major version.")]
        public string LegalName { get; set; }

        /// <summary>
        /// Not defined by any Accounts API schema. Retained so existing code keeps compiling.
        /// </summary>
        [Obsolete("Not defined by any Accounts API schema. Will be removed in a future major version.")]
        public string NationalTaxId { get; set; }
    }
}
