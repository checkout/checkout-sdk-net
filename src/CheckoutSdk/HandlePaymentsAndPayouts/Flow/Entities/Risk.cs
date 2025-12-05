namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Risk
    {
        /// <summary>
        /// Specifies whether to perform a risk assessment. Default: true
        /// </summary>
        public bool? Enabled { get; set; } = true;
    }
}