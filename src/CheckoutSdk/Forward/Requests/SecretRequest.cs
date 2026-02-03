namespace Checkout.Forward.Requests
{
    public class SecretRequest
    {
        /// <summary>
        /// Secret name. Format – 1-64 characters. Alphanumeric and underscore.
        /// [Required]
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Plaintext secret value. Max 8KB.
        /// [Required]
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// Optional. When provided, the secret is scoped to this entity.
        /// </summary>
        public string EntityId { get; set; }
    }
}