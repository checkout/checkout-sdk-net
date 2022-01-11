using Checkout.Common;

namespace Checkout.Sessions.Source
{
    public abstract class SessionSource
    {
        public SessionSourceType Type { get; set; }

        public SessionAddress BillingAddress { get; set; }

        public Phone HomePhone { get; set; }

        public Phone MobilePhone { get; set; }

        public Phone WorkPhone { get; set; }

        protected SessionSource(SessionSourceType type)
        {
            Type = type;
        }
    }
}