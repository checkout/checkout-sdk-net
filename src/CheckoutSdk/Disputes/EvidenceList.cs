namespace Checkout.Disputes
{
    public class EvidenceList
    {
        public string File { get; set; }
        
        public string Text { get; set; }
        
        public DisputeRelevantEvidence? Type { get; set; }
        
        public string DisputeId { get; set; }
    }
}