namespace Checkout.Instruments.Get
{
    /// <summary>
    /// An allowed option for a bank account field.
    /// </summary>
    public class BankAccountFieldAllowedOption
    {
        /// <summary>
        /// The option identifier.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The option display value.
        /// [Optional]
        /// </summary>
        public string Display { get; set; }
    }
}
