using System;

namespace Checkout.Payments
{
    public class IdSource : IPaymentSource
    {
        public const string TypeName = "id";

        public IdSource(string id, string cvv = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"Source ID required", nameof(id));

            Id = id;
            Cvv = cvv;
        }

        public string Id { get; }
        public string Cvv { get; }
        public string Type => TypeName;
    }
}
