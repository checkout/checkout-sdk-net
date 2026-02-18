using Checkout.StandaloneAccountUpdater.Entities;

namespace Checkout.StandaloneAccountUpdater.Requests
{
    public class GetUpdatedCardCredentialsRequest
    {
        /// <summary>
        /// The source to update. You must provide either card or instrument object, but not both.
        /// </summary>
        public SourceOptions SourceOptions { get; set; }
    }
}