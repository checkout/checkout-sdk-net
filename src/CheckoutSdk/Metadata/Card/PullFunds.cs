namespace Checkout.Metadata.Card
{
    public class PullFunds
    {
        /// <summary>
        /// Describes whether the card is eligible for Account Funding Transactions between accounts in different countries.
        /// </summary>
        public bool? CrossBorder { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for Account Funding Transactions between accounts in the same country.
        /// </summary>
        public bool? Domestic { get; set; }
    }
}
