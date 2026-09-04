namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The account configuration for a Bacs Direct Debit instrument being stored.
    /// </summary>
    public class CreateBacsInstrumentAccount
    {
        /// <summary>
        /// The ID of the processing channel to associate with the instrument.
        /// [Required]
        /// Pattern: ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }
    }
}
