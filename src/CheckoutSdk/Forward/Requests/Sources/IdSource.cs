namespace Checkout.Forward.Requests.Sources
{
    public class IdSource : AbstractSource
    {
        /// <summary> Initializes a new instance of the IdSource class. </summary>
        public IdSource() : base(SourceType.Id) { }

        /// <summary> The unique identifier of the payment instrument (Required, pattern ^(src)_(\w{26})$) </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The unique token for the card's security code. Checkout.com does not store a card's Card Verification Value
        ///     (CVV) with its associated payment instrument. To pass a CVV with your forward request, use the Frames SDK for
        ///     Android or iOS to collect and tokenize the CVV and pass the value in this field. The token will replace the
        ///     placeholder {{card_cvv}} value in destination_request.body (Optional, pattern ^(tok)_(\w{26})$)
        /// </summary>
        public string CvvToken { get; set; }
    }
}