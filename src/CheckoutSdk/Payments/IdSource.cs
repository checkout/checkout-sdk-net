using System;

namespace Checkout.Payments
{
    public class IdSource : IPaymentSource
    {
        public const string TypeName = "id";

        public IdSource(string id, string cvv = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"{nameof(id)} has to be provided.");
            Id = id;
        }

        public string Id { get; }
        public string Cvv { get; private set; }
        public string Type => TypeName;
    }
}
