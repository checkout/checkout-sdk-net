namespace Checkout.Accounts
{
    public class AccountsFilePurpose
    {
        private AccountsFilePurpose(string purpose)
        {
            Value = purpose;
        }

        public string Value { get; }

        public static AccountsFilePurpose BankVerification => new AccountsFilePurpose("bank_verification");

        public static AccountsFilePurpose Identification => new AccountsFilePurpose("identification");

        public static AccountsFilePurpose CompanyVerification => new AccountsFilePurpose("company_verification");

        public static AccountsFilePurpose FinancialVerification => new AccountsFilePurpose("financial_verification");
    }
}