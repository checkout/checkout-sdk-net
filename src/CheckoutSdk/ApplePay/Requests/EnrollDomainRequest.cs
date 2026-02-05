namespace Checkout.ApplePay.Requests
{
    public class EnrollDomainRequest
    {
        /// <summary>
        /// The domain to enroll
        /// </summary>
        /// [Required]
        public string Domain { get; set; }
    }
}