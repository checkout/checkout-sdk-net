using Checkout.Accounts;
using Checkout.Common;

namespace Checkout.Issuing.Cardholders
{
    public class CardholderDocument
    {
        public DocumentType? Type { get; set; }

        public string FrontDocumentId { get; set; }

        public string BackDocumentId { get; set; }
    }
}