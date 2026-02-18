namespace Checkout.StandaloneAccountUpdater.Entities
{
    public class CardDetails : CardBase
    {
        /// <summary>
        /// The card number
        /// [Required]
        /// </summary>
        public string Number { get; set; }
    }
}