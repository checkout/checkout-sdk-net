using Checkout.Accounts.Entities.Common.Company;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public class Documents
    {
        // Common
        
        public ArticlesOfAssociation ArticlesOfAssociation { get; set; }
        
        public ShareholderStructure ShareholderStructure { get; set; }
        
        public CompanyVerification CompanyVerification { get; set; }
        
        public BankVerification BankVerification { get; set; }
                
        public ProofOfLegality ProofOfLegality { get; set; }
        
        public ProofOfPrincipalAddress ProofOfPrincipalAddress { get; set; }
        
        public AdditionalDocument AdditionalDocument1 { get; set; }
        
        public AdditionalDocument AdditionalDocument2 { get; set; }
        
        public AdditionalDocument AdditionalDocument3 { get; set; }
        
        public IdentityVerification IdentityVerification { get; set; }
        
        // GB Company Full (3.0) Representatives
        
        public CertifiedAuthorisedSignatory CertifiedAuthorisedSignatory { get; set; }
        
        // US Company Full (3.0) Representatives
        
        public TaxVerification TaxVerification { get; set; }
        
        // Unknown
        
        public FinancialVerification FinancialVerification { get; set; }
    }
}