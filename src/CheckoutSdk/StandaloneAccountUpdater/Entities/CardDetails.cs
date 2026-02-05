namespace Checkout.StandaloneAccountUpdater.Entities
{
    public class CardDetails : CardBase
    {
        /// <summary>
        /// The card number
        /// </summary>
        /// [Required]
        public string Number { get; set; }
    }
}