namespace Checkout.Forward.Requests.Signatures
{
    public abstract class AbstractSignature
    {
        protected AbstractSignature(SignatureType type) { Type = type; }

        /// <summary>
        ///     The identifier of the supported signature generation method or a specific third-party service.
        ///     (Required)
        /// </summary>
        public SignatureType? Type { get; set; }
    }
}