using Newtonsoft.Json;

namespace Checkout.Identities.Entities
{
    public enum IdDocumentVerificationStatus
    {
        [JsonProperty("created")]
        Created,
        
        [JsonProperty("quality_checks_in_progress")]
        QualityChecksInProgress,
        
        [JsonProperty("checks_in_progress")]
        ChecksInProgress,
        
        [JsonProperty("approved")]
        Approved,
        
        [JsonProperty("declined")]
        Declined,
        
        [JsonProperty("retry_required")]
        RetryRequired,
        
        [JsonProperty("inconclusive")]
        Inconclusive
    }
}