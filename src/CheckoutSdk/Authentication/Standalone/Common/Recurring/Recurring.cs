namespace Checkout.Authentication.Standalone.Common.Recurring
{
    /// <summary>
    /// recurring
    /// Details of a recurring authentication. This property is needed only for a recurring authentication type. Value
    /// will be ignored in any other cases.
    /// </summary>
    public class Recurring
    {
        /// <summary>
        /// Default:  1 Indicates the minimum number of days between authorisations. If no value is specified for a
        /// recurring authentication type the default value will be used.
        /// [Optional]
        /// </summary>
        public int DaysBetweenPayments { get; set; } = 1;

        /// <summary>
        /// Default:  99991231 Date after which no further authorisations are performed in the format yyyyMMdd. If no
        /// value is specified for a recurring authentication type the default value will be used.
        /// [Optional]
        /// </summary>
        public string Expiry { get; set; } = "99991231";
    }
}