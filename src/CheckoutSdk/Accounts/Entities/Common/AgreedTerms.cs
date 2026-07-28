namespace Checkout.Accounts.Entities.Common
{
    public class AgreedTerms
    {
        /// <summary>
        /// Date and time the terms were agreed in RFC 3339 or ISO 8601 format.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// IP address (IPv4 or IPv6) of the person at the time they agreed the terms.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// First and last name of the person who agreed to the terms.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email address of the person who agreed to the terms.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identifier of the terms version that was agreed.
        /// </summary>
        public string Version { get; set; }
    }
}
