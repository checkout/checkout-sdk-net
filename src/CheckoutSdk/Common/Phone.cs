namespace Checkout.Common
{
    public class Phone
    {
        /// <summary>
        /// The international country calling code: https://www.checkout.com/docs/resources/codes/country-codes
        /// [ 1 .. 7 ] characters
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The phone number
        /// [ 6 .. 25 ] characters
        /// </summary>
        public string Number { get; set; }
    }
}