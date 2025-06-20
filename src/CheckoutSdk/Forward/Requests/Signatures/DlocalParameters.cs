namespace Checkout.Forward.Requests.Signatures
{
    public class DlocalParameters
    {
        /// <summary>
        ///     The secret key used to generate the request signature. This is part of the dLocal API credentials.
        /// </summary>
        public string SecretKey { get; set; }
    }
}