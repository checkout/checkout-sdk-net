using Checkout.Common;
using System;

namespace Checkout.Payments.Response
{
    public class PaymentInstructionResponse : Resource
    {
        public DateTime? ValueDate { get; set; }

        /// <summary>
        /// The scheme's categorisation of the client, for example FD, MT or AA.
        /// </summary>
        public string FundsTransferType { get; set; }
    }
}