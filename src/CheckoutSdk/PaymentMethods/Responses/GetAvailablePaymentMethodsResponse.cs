using System.Collections.Generic;

using Checkout.PaymentMethods.Entities;
using Checkout.Common;

namespace Checkout.PaymentMethods.Responses
{
    public class GetAvailablePaymentMethodsResponse : Resource
    {
        /// <summary>
        /// The enabled payment methods for the processing channel
        /// [Required]
        /// </summary>
        /// [Required]
        public IList<PaymentMethod> Methods { get; set; }
    }
}