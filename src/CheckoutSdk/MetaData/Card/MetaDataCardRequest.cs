namespace Checkout.MetaData.Card
{
    public class MetaDataCardRequest
    {
        public string Bin { get; set; }
        
        public string Number { get; set; }
        
        public string TokenId { get; set; }
        
        public string InstrumentId { get; set; }

        public MetaDataCardFormatType? Format { get; set; } = MetaDataCardFormatType.Basic;
    }
}