namespace Checkout.Issuing.Cards.Type
{
    public abstract class CardTypeRequest
    {
        public CardType? Type { get; set; }

        public string CardholderId { get; set; }

        public CardLifetime Lifetime { get; set; }

        public string Reference { get; set; }

        public string CardProductId { get; set; }

        public string DisplayName { get; set; }

        public bool ActivateCard { get; set; }

        protected CardTypeRequest(CardType type)
        {
            Type = type;
        }
    }
}