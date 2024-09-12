namespace Checkout.Accounts
{
    public class OnboardSubEntityDocuments
    {
        public Document IdentityVerification { get; set; }

        public CompanyVerification CompanyVerification { get; set; }

        public ArticlesOfAssociationType? ArticlesOfAssociation { get; set; }
        
        public BankVerification BankVerification { get; set; }
        
        public ShareholderStructure ShareholderStructure { get; set; }
        
        public ProofOfLegality ProofOfLegality { get; set; }
        
        public ProofOfPrincipalAddress ProofOfPrincipalAddress { get; set; }
        
        public AdditionalDocument AdditionalDocument1 { get; set; }
        
        public AdditionalDocument AdditionalDocument2 { get; set; }
        
        public AdditionalDocument AdditionalDocument3 { get; set; }
        
        public TaxVerification TaxVerification { get; set; }
        
        public FinancialVerification FinancialVerification { get; set; }
    }
}