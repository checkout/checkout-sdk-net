using Checkout.Common;

namespace Checkout.Issuing.Cardholders.Requests
{
    public class CardholderDocument
    {
        public DocumentType? Type { get; set; }

        public string FrontDocumentId { get; set; }

        public string BackDocumentId { get; set; }
    }
}