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
            ValidateSecret(secret);
            Secret = secret;
        }

        private const int MaxSecretLength = 4096;

        // The secret is sent as the whole Authorization value. Only token characters
        // (RFC 7235 token68) are accepted, and the message never echoes the value.
        private static void ValidateSecret(string secret)
        {
            if (secret.Length > MaxSecretLength)
            {
                throw new CheckoutArgumentException("secret exceeds the maximum length");
            }

            foreach (var c in secret)
            {
                var allowed = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') ||
                              c == '-' || c == '.' || c == '_' || c == '~' || c == '+' || c == '/' || c == '=';
                if (!allowed)
                {
                    throw new CheckoutArgumentException("secret contains characters that are not allowed");
                }
            }
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