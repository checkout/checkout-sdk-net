using System.Collections.Generic;

namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for identity responses with response codes
    /// </summary>
    public abstract class BaseWithCodesResponse : BaseResponse
    {
        /// <summary>
        /// One or more response codes that provide more information about the status
        /// [Required]
        /// </summary>
        public List<ResponseCode> ResponseCodes { get; set; }
    }
}