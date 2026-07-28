using Checkout.Accounts.Entities.Common.Company;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public class Documents
    {
        // Common

        /// <summary>
        /// Memorandum or Articles of Association document.
        /// </summary>
        public ArticlesOfAssociation ArticlesOfAssociation { get; set; }

        /// <summary>
        /// Shareholder structure chart including the percentage of shares certified by a competent
        /// authority individual and dated within the last 3 months.
        /// </summary>
        public ShareholderStructure ShareholderStructure { get; set; }

        /// <summary>
        /// Certified by a power of attorney within the last 3 months.
        /// </summary>
        public CompanyVerification CompanyVerification { get; set; }

        /// <summary>
        /// A document showing transactions from the last 3 months.
        /// </summary>
        public BankVerification BankVerification { get; set; }

        /// <summary>
        /// A regulatory license document required for the company to operate if applicable.
        /// </summary>
        public ProofOfLegality ProofOfLegality { get; set; }

        /// <summary>
        /// Proof of principal place of business.
        /// </summary>
        public ProofOfPrincipalAddress ProofOfPrincipalAddress { get; set; }

        /// <summary>
        /// An additional supporting document (slot 1).
        /// </summary>
        public AdditionalDocument AdditionalDocument1 { get; set; }

        /// <summary>
        /// An additional supporting document (slot 2).
        /// </summary>
        public AdditionalDocument AdditionalDocument2 { get; set; }

        /// <summary>
        /// An additional supporting document (slot 3).
        /// </summary>
        public AdditionalDocument AdditionalDocument3 { get; set; }

        /// <summary>
        /// The document to use to confirm the individual's identity.
        /// </summary>
        public IdentityVerification IdentityVerification { get; set; }

        // GB Company Full (3.0) Representatives

        /// <summary>
        /// Required for representatives with the <c>authorised_signatory</c> role, when the legal
        /// representative or other role owner is not registered on the certificate of incorporation.
        /// </summary>
        public CertifiedAuthorisedSignatory CertifiedAuthorisedSignatory { get; set; }

        // US Company Full (3.0) Representatives

        /// <summary>
        /// IRS-issued document used to verify the entity's tax identification.
        /// </summary>
        public TaxVerification TaxVerification { get; set; }

        // EEA Sole Trader (3.0) Representatives

        /// <summary>
        /// Proof of residential address of the representative.
        /// </summary>
        public ProofOfResidentialAddress ProofOfResidentialAddress { get; set; }

        /// <summary>
        /// Proof of the sole trader's registration, for example an extract from a trade register.
        /// </summary>
        public ProofOfRegistration ProofOfRegistration { get; set; }

        // Unknown

        /// <summary>
        /// Financial statement document. Becomes mandatory depending on the answer provided for
        /// <c>annual_processing_volume</c>; the sub-entity's status will change to
        /// <c>requirements_due</c> when this is necessary.
        /// </summary>
        public FinancialVerification FinancialVerification { get; set; }

        /// <summary>
        /// Audited or management-prepared financial statements (when applicable).
        /// </summary>
        public FinancialStatements FinancialStatements { get; set; }
    }
}
