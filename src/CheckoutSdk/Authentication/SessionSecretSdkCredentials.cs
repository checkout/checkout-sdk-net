namespace Checkout.Authentication
{
    /// <summary>
    /// Credentials that send a session secret as the whole Authorization header value.
    /// </summary>
    public class SessionSecretSdkCredentials : SdkCredentials
    {
        /// <summary>
        /// The session secret.
        /// </summary>
        public string Secret { get; }

        /// <summary>
        /// Creates credentials from a session secret.
        /// </summary>
        /// <param name="secret">The session secret; up to 4096 characters, limited to ASCII letters, digits and the characters . _ ~ + / = and the hyphen.</param>
        /// <exception cref="CheckoutArgumentException">The secret is null or blank, longer than 4096 characters, or contains a character that is not allowed.</exception>
        public SessionSecretSdkCredentials(string secret) : base(PlatformType.Custom)
        {
            CheckoutUtils.ValidateParams("secret", secret);
            CheckoutUtils.ValidateToken("secret", secret);
            Secret = secret;
        }

        /// <summary>
        /// Returns an authorization that carries the session secret.
        /// </summary>
        /// <param name="authorizationType">The authorization type required by the operation; must be Custom.</param>
        /// <returns>The authorization with the session secret.</returns>
        /// <exception cref="CheckoutAuthorizationException">The authorization type is not Custom.</exception>
        public override SdkAuthorization GetSdkAuthorization(SdkAuthorizationType authorizationType)
        {
            if (SdkAuthorizationType.Custom.Equals(authorizationType))
            {
                return new SdkAuthorization(PlatformType, Secret);
            }

            throw CheckoutAuthorizationException.InvalidAuthorization(authorizationType);
        }
    }
}