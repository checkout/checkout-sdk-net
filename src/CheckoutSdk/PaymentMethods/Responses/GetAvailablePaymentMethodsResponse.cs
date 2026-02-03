using System.Collections.Generic;

using Checkout.Common;

namespace Checkout.PaymentMethods.Responses
{
    public class GetAvailablePaymentMethodsResponse : Resource
    {
        /// <summary>
        /// The enabled payment methods for the processing channel
        /// [Required]
        /// </summary>
        public IList<PaymentMethod> Methods { get; set; }
    }
}