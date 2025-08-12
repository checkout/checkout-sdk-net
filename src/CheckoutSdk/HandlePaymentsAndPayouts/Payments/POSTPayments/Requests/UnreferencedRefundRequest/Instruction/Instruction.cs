namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Instruction
{
    /// <summary>
    /// instruction
    /// Additional details about the unreferenced refund instruction.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Required if processing.processing_speed is fast.
        /// Value of this field should be based on the destination card scheme:
        /// For Visa, use MI
        /// For Mastercard, use C60
        /// [Optional]
        /// </summary>
        public string FundsTransferType { get; set; }

        /// <summary>
        /// The purpose of the unreferenced refund.
        /// This field is required if the card's issuer_country is one of:
        /// AR (Argentina)
        /// BD (Bangladesh)
        /// CL (Chile)
        /// CO (Colombia)
        /// EG (Egypt)
        /// IN (India)
        /// MX (Mexico)
        /// To view a card's issuer_country, retrieve the card's metadata.
        /// [Optional]
        /// </summary>
        public PurposeType? Purpose { get; set; }
    }
}