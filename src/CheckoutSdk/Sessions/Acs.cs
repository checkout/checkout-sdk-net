namespace Checkout.Sessions
{
    public class Acs
    {
        public string ReferenceNumber { get; set; }

        public string TransactionId { get; set; }

        public string OperatorId { get; set; }
        
        public string Url { get; set; }

        public string SignedContent { get; set; }

        public bool? ChallengeMandated { get; set; }

        public string AuthenticationType { get; set; }

        public ChallengeCancelReason? ChallengeCancelReason { get; set; }

        public SessionInterface? Interface { get; set; }

        public UIElements? UiTemplate { get; set; }

        public string ChallengeCancelReasonCode { get; set; }
    }
}