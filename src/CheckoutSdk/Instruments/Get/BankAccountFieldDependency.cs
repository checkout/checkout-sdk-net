namespace Checkout.Instruments.Get
{
    /// <summary>
    /// A dependency that controls whether a bank account field is displayed.
    /// </summary>
    public class BankAccountFieldDependency
    {
        /// <summary>
        /// The field identifier.
        /// [Optional]
        /// </summary>
        public string FieldId { get; set; }

        /// <summary>
        /// The value of the dependent field that must match in order for this field to be
        /// displayed.
        /// [Optional]
        /// </summary>
        public string Value { get; set; }
    }
}
