namespace Checkout.Accounts.Entities.Common.Documents
{
    public class BankVerification
    {
        public BankVerificationType? Type { get; set; }

        public string Front { get; set; }
    }
}