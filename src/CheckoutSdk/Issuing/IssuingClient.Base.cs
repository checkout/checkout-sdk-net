namespace Checkout.Issuing
{
    public partial class IssuingClient : AbstractClient, IIssuingClient
    {
        private const string IssuingPath = "issuing";
        private const string CardholdersPath = "cardholders";
        private const string CardsPath = "cards";
        private const string ThreeDSEnrollmentPath = "3ds-enrollment";
        private const string ActivatePath = "activate";
        private const string Credentials = "credentials";
        private const string Revoke = "revoke";
        private const string Suspend = "suspend";
        private const string ControlsPath = "controls";
        private const string SimulatePath = "simulate";
        private const string AuthorizationPath = "authorizations";
        private const string PresentmentsPath = "presentments";
        private const string ReversalsPath = "reversals";

        public IssuingClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }
    }
}