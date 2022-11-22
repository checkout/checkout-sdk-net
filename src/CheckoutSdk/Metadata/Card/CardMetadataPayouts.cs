namespace Checkout.Metadata.Card
{
    public class CardMetadataPayouts
    {
        public PayoutsTransactionsType? DomesticNonMoneyTransfer { get; set; }

        public PayoutsTransactionsType? CrossBorderNonMoneyTransfer { get; set; }

        public PayoutsTransactionsType? DomesticGambling { get; set; }

        public PayoutsTransactionsType? CrossBorderGambling { get; set; }

        public PayoutsTransactionsType? DomesticMoneyTransfer { get; set; }

        public PayoutsTransactionsType? CrossBorderMoneyTransfer { get; set; }
    }
}