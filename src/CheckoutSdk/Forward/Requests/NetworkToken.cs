namespace Checkout.Forward.Requests
{
    public class NetworkToken
    {
        /// <summary> Specifies whether to use a network token (Optional) </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        ///     Specifies whether to generate a cryptogram. For example, for customer-initiated transactions (CITs). If you
        ///     set network_token.enabled to true, you must provide this field (Optional)
        /// </summary>
        public bool? RequestCryptogram { get; set; }
    }
}