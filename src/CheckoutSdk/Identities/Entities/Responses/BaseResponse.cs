using System;
using Checkout.Common;

namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for all identity responses
    /// </summary>
    public abstract class BaseResponse : Resource
    {
        /// <summary>
        /// The unique identifier
        /// [Required]
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// [Required]
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        
        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// [Required]
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}