namespace Checkout.MetaData.Card
{
    public class MetaDataCardPayouts
    {
        public CardPayoutsTransferType? DomesticNonMoneyTransfer { get; set; }

        public CardPayoutsTransferType? CrossBorderNonMoneyTransfer { get; set; }
        
        public CardPayoutsTransferType? DomesticGambling { get; set; }
        
        public CardPayoutsTransferType? CrossBorderGambling { get; set; }
        
        public CardPayoutsTransferType? DomesticMoneyTransfer { get; set; }
        
        public CardPayoutsTransferType? CrossBorderMoneyTransfer { get; set; }
        
    }
}