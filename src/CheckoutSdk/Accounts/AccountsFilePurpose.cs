namespace Checkout.Accounts
{
    public class AccountsFilePurpose
    {
        private AccountsFilePurpose(string purpose)
        {
            Value = purpose;
        }

        public string Value { get; }
        
        public static AccountsFilePurpose AdditionalDocument => new AccountsFilePurpose("additional_document");
        
        public static AccountsFilePurpose ArticlesOfAssociation => new AccountsFilePurpose("articles_of_association");

        public static AccountsFilePurpose BankVerification => new AccountsFilePurpose("bank_verification");
        
        public static AccountsFilePurpose CertifiedAuthorisedSignatory => new AccountsFilePurpose("certified_authorised_signatory");
        
        public static AccountsFilePurpose CompanyOwnership => new AccountsFilePurpose("company_ownership");

        public static AccountsFilePurpose Identification => new AccountsFilePurpose("identification");
        
        public static AccountsFilePurpose IdentityVerification => new AccountsFilePurpose("identity_verification");
        
        public static AccountsFilePurpose DisputeEvidence => new AccountsFilePurpose("dispute_evidence");

        public static AccountsFilePurpose CompanyVerification => new AccountsFilePurpose("company_verification");

        public static AccountsFilePurpose FinancialVerification => new AccountsFilePurpose("financial_verification");
        
        public static AccountsFilePurpose TaxVerification => new AccountsFilePurpose("tax_verification");
        
        public static AccountsFilePurpose ProofOfLegality => new AccountsFilePurpose("proof_of_legality");
        
        public static AccountsFilePurpose ProofOfPrincipalAddress => new AccountsFilePurpose("proof_of_principal_address");
        
        public static AccountsFilePurpose ShareholderStructure => new AccountsFilePurpose("shareholder_structure");
        
        public static AccountsFilePurpose ProofOfResidentialAddress => new AccountsFilePurpose("proof_of_residential_address");
        
        public static AccountsFilePurpose ProofOfRegistration => new AccountsFilePurpose("proof_of_registration");
        
    }
}