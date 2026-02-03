namespace Checkout.StandaloneAccountUpdater.Requests
{
    public class SourceOptions
    {
        /// <summary>
        /// The card details
        /// </summary>
        public CardDetails Card { get; set; }

        /// <summary>
        /// Instrument reference
        /// </summary>
        public InstrumentReference Instrument { get; set; }
    }
}