namespace Checkout.NetworkTokens.Common.Responses
{
    public class NetworkToken
    {
        /// <summary> Unique token ID assigned by Checkout.com for each token (Required) </summary>
        public string Id { get; set; }

        /// <summary> Token status (Required) </summary>
        public StateType State { get; set; }

        /// <summary>
        /// The network token number. This field is only returned when the network token status is one of the
        /// following: active, suspended, inactive (Constraints: ^[0-9]+$)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The network token's expiration month. This field is only returned when the network token status is
        /// one of the following: active, suspended, inactive (Constraints: ^[0-9]{1,2}$)
        /// </summary>
        public string ExpiryMonth { get; set; }

        /// <summary>
        /// The network token's expiration year. This field is only returned when the network token status is
        /// one of the following: active, suspended, inactive (Constraints: ^[0-9]{4}$)
        /// </summary>
        public string ExpiryYear { get; set; }

        /// <summary> The type of token (Required) </summary>
        public NetworkTokenType Type { get; set; }

        /// <summary> When the network token was created (Required) </summary>
        public string CreatedOn { get; set; }

        /// <summary> When the network token was modified (Required) </summary>
        public string ModifiedOn { get; set; }

        /// <summary>
        /// Unique Payment account reference value assigned to payment account. All affiliated payment tokens,
        /// as well as the underlying PAN, have the same payment_account_reference (Optional)
        /// </summary>
        public string PaymentAccountReference { get; set; }

    }
}
