namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The account configuration for a stored Bacs Direct Debit instrument.
    /// </summary>
    public class GetBacsInstrumentAccount
    {
        /// <summary>
        /// The ID of the client associated with the instrument.
        /// [Optional]
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The ID of the processing channel associated with the instrument.
        /// [Optional]
        /// </summary>
        public string ProcessingChannelId { get; set; }
    }
}
