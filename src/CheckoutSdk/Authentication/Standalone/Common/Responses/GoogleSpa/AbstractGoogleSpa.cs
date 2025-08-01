namespace Checkout.Authentication.Standalone.Common.Responses.GoogleSpa
{
    /// <summary>
    /// Abstract google_spa Class
    /// Details of Google SPA (Secure Payment Authentication)
    /// </summary>
    public abstract class AbstractGoogleSpa
    {
        public GoogleSpaType? Type;

        protected AbstractGoogleSpa(GoogleSpaType type)
        {
            Type = type;
        }
    }
}
