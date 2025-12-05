namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Commission
    {
        /// <summary>
        /// An optional commission to collect, as a fixed amount in minor currency units.
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// An optional commission to collect, as a percentage value with up to eight decimal places.
        /// </summary>
        public decimal? Percentage { get; set; }
    }
}