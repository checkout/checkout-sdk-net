namespace Checkout.Common
{
    /// <summary>
    /// Defines a phone number.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Gets or sets the phone number country code.
        /// </summary>
        /// <example>+44</example>
        public string CountryCode {get;set;}
        
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <example>415 555 2671</example>
        public string Number {get;set;}
    }
}