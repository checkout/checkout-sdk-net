namespace Checkout.Forward.Requests.Signatures
{
    public class DlocalSignature : AbstractSignature
    {
        /// <summary> Initializes a new instance of the DlocalSignature class. </summary>
        public DlocalSignature() : base(SignatureType.Dlocal) { }
        
        /// <summary>
        ///     The parameters required to generate an HMAC signature for the dLocal API. See their documentation for
        ///     details.
        ///     This method requires you to provide the X-Login header value in the destination request headers.
        ///     When used, the Forward API appends the X-Date and Authorization headers to the outgoing HTTP request
        ///     before forwarding.
        /// </summary>
        public DlocalParameters DlocalParameters { get; set; }
    }
}