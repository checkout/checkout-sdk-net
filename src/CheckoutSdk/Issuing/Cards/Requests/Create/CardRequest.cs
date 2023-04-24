namespace Checkout.Issuing.Cards.Requests.Create
{
    public abstract class CardRequest
    {
        public CardType? Type { get; set; }

        public string CardholderId { get; set; }

        public CardLifetime Lifetime { get; set; }

        public string Reference { get; set; }

        public string CardProductId { get; set; }

        public string DisplayName { get; set; }

        public bool ActivateCard { get; set; }

        protected CardRequest(CardType type)
        {
            Type = type;
        }
    }
}