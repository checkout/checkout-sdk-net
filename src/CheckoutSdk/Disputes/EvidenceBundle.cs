using System;

namespace Checkout.Disputes
{
    public class EvidenceBundle
    {
        public long? DisputeId { get; set; }

        public string Filename { get; set; }
        
        public long? FileSize { get; set; }
        
        public bool? IsFileOversized { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
    }
}