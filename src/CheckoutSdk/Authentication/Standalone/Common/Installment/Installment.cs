namespace Checkout.Authentication.Standalone.Common.Installment
{
    /// <summary>
    /// installment
    /// Details of an installment authentication. This property is needed only for an installment authentication type.
    /// Value will be ignored in any other cases.
    /// </summary>
    public class Installment
    {
        /// <summary>
        /// Indicates the agreed total number of payment installments to be made in the duration of the installment
        /// agreement. Required when the authentication type is instalment.
        /// [Required]
        /// <= 3
        /// >= 2
        /// </summary>
        public int NumberOfPayments { get; set; }

        /// <summary>
        /// Default: 1 Indicates the minimum number of days between authorisations. If no value is specified for an
        /// installment authentication type the default value will be used.
        /// [Optional]
        /// </summary>
        public int? DaysBetweenPayments { get; set; } = 1;

        /// <summary>
        /// Default: 99991231 Date after which no further authorisations are performed in the format yyyyMMdd. If no
        /// value is specified for an installment authentication type the default value will be used.
        /// [Optional]
        /// </summary>
        public string Expiry { get; set; } = "99991231";
    }
}