namespace Checkout.Identities.Entities
{
    public class ResponseCode
    {
        /// <summary>
        /// The response code
        /// [Required]
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The description of the response code
        /// [Required]
        /// </summary>
        public string Summary { get; set; }
    }
}


