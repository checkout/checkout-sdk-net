using Checkout.Common;

namespace Checkout.Accounts
{
    public class AccountsCorporateAccountHolder : AccountsAccountHolder
    {
        public AccountsCorporateAccountHolder() : base(AccountHolderType.Corporate) { }
        
        public string CompanyName { get; set; }
    }
}