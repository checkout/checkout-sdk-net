namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Verification documents for a sub-entity. This one type serves two different objects on the
    /// Accounts API, which accept different keys:
    /// <list type="bullet">
    /// <item><description>The top-level request <c>documents</c> (<see cref="Request.OnboardEntityRequest.Documents"/>).
    /// The API ignores keys it does not recognise here rather than rejecting them, so a misplaced
    /// document is dropped silently.</description></item>
    /// <item><description>A representative's <c>documents</c> (<see cref="Company.Representative.Documents"/>).
    /// It defines only <see cref="IdentityVerification"/>, <see cref="CertifiedAuthorisedSignatory"/>
    /// (EEA, GB and US Company Full (3.0) and US ISV Seller Company (3.0)),
    /// <see cref="ProofOfResidentialAddress"/> and <see cref="ProofOfRegistration"/> (EEA Sole Trader
    /// Full (3.0)). The schema is strict, rejecting any key the variant does not define, only on the
    /// EEA, GB and US Company Full (3.0) person of interest and the EEA, GB and US Sole Trader Full
    /// (3.0) variants; it is not strict on the US ISV Seller variants (3.0) or on any v2.0
    /// variant.</description></item>
    /// </list>
    /// Each property below says which of the two it belongs to.
    /// </summary>
    public class Documents
    {
        // Top level

        /// <summary>
        /// Memorandum or Articles of Association document. Top level only.
        /// [Required] for EEA and GB Company Full (3.0); [Optional] for US Company Full (3.0) and the
        /// US ISV Seller variants.
        /// </summary>
        public ArticlesOfAssociation ArticlesOfAssociation { get; set; }

        /// <summary>
        /// Shareholder structure chart (including % of shares) certified by a competent authority
        /// individual and dated within the last 3 months. Top level only.
        /// [Required] for EEA and GB Company Full (3.0); [Optional] for US Company Full (3.0) and US
        /// ISV Seller Company (3.0).
        /// </summary>
        public ShareholderStructure ShareholderStructure { get; set; }

        /// <summary>
        /// The document to use to confirm the company's identity (certified by a power of attorney
        /// within the last 3 months). Top level only.
        /// [Required] for EEA Company Full (2.0 and 3.0) and GB Company Full (2.0); [Optional] for the
        /// other company variants and the US ISV Seller variants.
        /// </summary>
        public CompanyVerification CompanyVerification { get; set; }

        /// <summary>
        /// A document showing transactions from the last 3 months. Top level only.
        /// [Required] for EEA Company Full (3.0) and the EEA, GB and US Sole Trader Full (3.0)
        /// variants; [Optional] for GB and US Company Full (3.0) and EEA Company Full and Lite (2.0).
        /// </summary>
        public BankVerification BankVerification { get; set; }

        /// <summary>
        /// A regulatory licence document required for the company to operate (when applicable). Top
        /// level only.
        /// [Optional] (EEA, GB and US Company Full (3.0) and the US ISV Seller variants)
        /// </summary>
        public ProofOfLegality ProofOfLegality { get; set; }

        /// <summary>
        /// Proof of the company's principal place of business. Top level only.
        /// [Optional] (EEA, GB and US Company Full (3.0) and the US ISV Seller variants)
        /// </summary>
        public ProofOfPrincipalAddress ProofOfPrincipalAddress { get; set; }

        /// <summary>
        /// Additional space for documents to be provided when requested. Top level only.
        /// [Optional] (EEA, GB and US Company and Sole Trader Full (3.0); not the US ISV Seller variants)
        /// </summary>
        public AdditionalDocument AdditionalDocument1 { get; set; }

        /// <summary>
        /// Additional space for documents to be provided when requested. Top level only.
        /// [Optional] (EEA, GB and US Company and Sole Trader Full (3.0); not the US ISV Seller variants)
        /// </summary>
        public AdditionalDocument AdditionalDocument2 { get; set; }

        /// <summary>
        /// Additional space for documents to be provided when requested. Top level only.
        /// [Optional] (EEA, GB and US Company and Sole Trader Full (3.0); not the US ISV Seller variants)
        /// </summary>
        public AdditionalDocument AdditionalDocument3 { get; set; }

        /// <summary>
        /// IRS-issued Employer Identification Number document used to verify the entity's tax
        /// identification. Top level only.
        /// [Optional] (US Company variants and the US ISV Seller variants only)
        /// </summary>
        public TaxVerification TaxVerification { get; set; }

        /// <summary>
        /// Financial statement document. Becomes mandatory depending on the answer provided for
        /// <c>annual_processing_volume</c>; the sub-entity's status changes to
        /// <c>requirements_due</c> when it is needed. Top level only.
        /// [Optional] (EEA Company Full and Lite (2.0) only)
        /// </summary>
        public FinancialVerification FinancialVerification { get; set; }

        /// <summary>
        /// Audited or management-prepared financial statements (when applicable). Top level only.
        /// [Optional] (US ISV Seller variants only)
        /// </summary>
        public FinancialStatements FinancialStatements { get; set; }

        // Both

        /// <summary>
        /// The document to use to confirm the individual's identity. Valid in both objects:
        /// <list type="bullet">
        /// <item><description>Representative: [Required] for the EEA, GB and US Sole Trader Full (3.0)
        /// variants; [Optional] for the company variants.</description></item>
        /// <item><description>Top level: [Required] for the six sole trader variants of Accounts API
        /// v2.0, the only variants that take it there.</description></item>
        /// </list>
        /// </summary>
        public IdentityVerification IdentityVerification { get; set; }

        // Representative only

        /// <summary>
        /// Certified authorised signatory document. Required when the legal representative or other
        /// role owner is not registered on the certificate of incorporation. Representative only
        /// (<c>company.representatives[].documents</c>); not accepted at the top level.
        /// [Optional] (EEA, GB and US Company Full (3.0) and US ISV Seller Company (3.0))
        /// </summary>
        public CertifiedAuthorisedSignatory CertifiedAuthorisedSignatory { get; set; }

        /// <summary>
        /// Proof of residential address of the representative. Representative only
        /// (<c>company.representatives[].documents</c>); not accepted at the top level.
        /// [Required] for EEA Sole Trader Full (3.0), and only valid there.
        /// </summary>
        public ProofOfResidentialAddress ProofOfResidentialAddress { get; set; }

        /// <summary>
        /// Proof of the sole trader's registration, for example an extract from a trade register.
        /// Representative only (<c>company.representatives[].documents</c>); not accepted at the top
        /// level.
        /// [Required] for EEA Sole Trader Full (3.0), and only valid there.
        /// </summary>
        public ProofOfRegistration ProofOfRegistration { get; set; }
    }
}