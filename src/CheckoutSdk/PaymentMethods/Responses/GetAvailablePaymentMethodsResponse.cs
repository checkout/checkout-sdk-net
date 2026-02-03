using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.PaymentMethods.Responses
{
    public class GetAvailablePaymentMethodsResponse : Resource
    {
        /// <summary>
        /// The enabled payment methods for the processing channel
        /// </summary>
        public IList<PaymentMethod> Methods { get; set; }
    }
}