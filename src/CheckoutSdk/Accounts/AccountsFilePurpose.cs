using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum AccountsFilePurpose
    {        
        [EnumMember(Value = "additional_document")]
        AdditionalDocument,
        
        [EnumMember(Value = "articles_of_association")]
        ArticlesOfAssociation,

        [EnumMember(Value = "bank_verification")]
        BankVerification,
        
        [EnumMember(Value = "certified_authorised_signatory")]
        CertifiedAuthorisedSignatory,
        
        [EnumMember(Value = "company_ownership")]
        CompanyOwnership,

        [EnumMember(Value = "identification")]
        Identification,
        
        [EnumMember(Value = "identity_verification")]
        IdentityVerification,
        
        [EnumMember(Value = "dispute_evidence")]
        DisputeEvidence,

         [EnumMember(Value = "company_verification")]
         CompanyVerification,

        [EnumMember(Value = "financial_verification")]
        FinancialVerification,
        
        [EnumMember(Value = "tax_verification")]
        TaxVerification,
        
        [EnumMember(Value = "proof_of_legality")]
        ProofOfLegality,
        
        [EnumMember(Value = "proof_of_principal_address")]
        ProofOfPrincipalAddress,
        
        [EnumMember(Value = "shareholder_structure")]
        ShareholderStructure,
        
        [EnumMember(Value = "proof_of_residential_address")]
        ProofOfResidentialAddress,
        
        [EnumMember(Value = "proof_of_registration")]
        ProofOfRegistration        
    }
}