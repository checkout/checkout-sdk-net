using Checkout.Common;
using System;

namespace Checkout.Forward.Responses
{
    public class SecretResponse : Resource
    {
        /// <summary>
        /// Secret name (1-64 characters, alphanumeric and underscore)
        /// [Required]
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Timestamp when the secret was created.
        /// [Required]
        /// </summary>
        public DateTime? CreatedAt { get; set; }
        
        /// <summary>
        /// Timestamp when the secret was last updated.
        /// [Required]
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// Version number.
        /// [Required]
        /// </summary>
        public int? Version { get; set; }
        
        /// <summary>
        /// Entity ID if the secret is scoped to a specific entity.
        /// </summary>
        public string EntityId { get; set; }
    }
}