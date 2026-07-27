using Checkout.Common;

namespace Checkout.Accounts.Entities.Common.Company
{
    public class Citizenship
    {
        /// <summary>
        /// The type of citizenship or legal status (for example, <c>citizenship</c> or <c>residency</c>).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The two-letter ISO 3166-1 alpha-2 country code.
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
