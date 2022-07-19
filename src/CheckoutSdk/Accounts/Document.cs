namespace Checkout.Accounts
{
    public class Document
    {
        public DocumentType? Type { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }
    }
}