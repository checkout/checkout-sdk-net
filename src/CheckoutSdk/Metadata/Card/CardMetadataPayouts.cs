namespace Checkout.Metadata.Card
{
    public class CardMetadataPayouts
    {
        /// <summary>
        /// Describes whether the card is eligible for domestic non-money transfer transactions.
        /// </summary>
        public PayoutsTransactionsType? DomesticNonMoneyTransfer { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for cross-border non-money transfer transactions.
        /// </summary>
        public PayoutsTransactionsType? CrossBorderNonMoneyTransfer { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for domestic gambling transactions.
        /// </summary>
        public PayoutsTransactionsType? DomesticGambling { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for cross-border gambling transactions.
        /// </summary>
        public PayoutsTransactionsType? CrossBorderGambling { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for domestic money transfer transactions.
        /// </summary>
        public PayoutsTransactionsType? DomesticMoneyTransfer { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for cross-border money transfer transactions.
        /// </summary>
        public PayoutsTransactionsType? CrossBorderMoneyTransfer { get; set; }
    }
}
