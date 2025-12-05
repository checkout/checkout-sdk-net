using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class AirlineData
    {
        public PaymentContextsTicket Ticket { get; set; }

        public IList<PaymentContextsPassenger> Passengers { get; set; }

        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }
    }








}