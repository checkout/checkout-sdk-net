namespace Checkout.Common
{
    public class AccountHolderResponse : AccountHolderBase
    {
        public AccountNameInquiryType? AccountNameInquiry { get; set; }
        
        public AccountNameInquiryDetails AccountNameInquiryDetails { get; set; }
    }
}