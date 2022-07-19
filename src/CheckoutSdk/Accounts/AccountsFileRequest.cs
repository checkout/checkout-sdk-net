using Checkout.Common;

namespace Checkout.Accounts
{
    public class AccountsFileRequest : AbstractFileRequest
    {
        public AccountsFilePurpose Purpose { get; set; }
    }
}