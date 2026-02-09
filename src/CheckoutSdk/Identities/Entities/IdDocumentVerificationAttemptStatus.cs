using Newtonsoft.Json;

namespace Checkout.Identities.Entities
{
    public enum IdDocumentVerificationAttemptStatus
    {
        [JsonProperty("checks_in_progress")]
        ChecksInProgress,
        
        [JsonProperty("checks_inconclusive")]
        ChecksInconclusive,
        
        [JsonProperty("completed")]
        Completed,
        
        [JsonProperty("quality_checks_aborted")]
        QualityChecksAborted,
        
        [JsonProperty("quality_checks_in_progress")]
        QualityChecksInProgress,
        
        [JsonProperty("terminated")]
        Terminated
    }
}