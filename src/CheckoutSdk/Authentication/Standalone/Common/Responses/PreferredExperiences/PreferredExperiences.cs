using Newtonsoft.Json;

namespace Checkout.Authentication.Standalone.Common.Responses.PreferredExperiences
{
    /// <summary>
    /// preferred_experiences
    /// Preferred Experiences
    /// </summary>
    public class PreferredExperiences
    {
        /// <summary>
        /// Google SPA experience.
        /// [Optional]
        /// </summary>
        public GoogleSpa.GoogleSpa GoogleSpa { get; set; }

        /// <summary>
        /// 3DS experience.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "3ds")] 
        public Threeds.Threeds Threeds { get; set; }
    }
}