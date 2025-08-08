namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder.IndividualAccountHolder.AccountNameInquiryDetails
{
    /// <summary>
    /// account_name_inquiry_details
    /// Details of the Account Name Inquiry check.
    /// </summary>
    public class AccountNameInquiryDetails
    {

        /// <summary>
        /// The result of the first name check.
        /// [Optional]
        /// </summary>
        public FirstNameType? FirstName { get; set; }

        /// <summary>
        /// The result of the middle name check.
        /// [Optional]
        /// </summary>
        public MiddleNameType? MiddleName { get; set; }

        /// <summary>
        /// The result of the last name check.
        /// [Optional]
        /// </summary>
        public LastNameType? LastName { get; set; }

    }
}
