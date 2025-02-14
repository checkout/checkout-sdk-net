using Checkout.Common;

namespace Checkout.Accounts
{
    public class AccountsIndividualAccountHolder : AccountsAccountHolder
    {
        public AccountsIndividualAccountHolder() : base(AccountHolderType.Individual) { }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}