using System.Text.RegularExpressions;

namespace Checkout
{
    public abstract class AbstractStaticKeysSdkCredentials : SdkCredentials
    {
        internal string SecretKey { get; }
        internal string PublicKey { get; }

        protected AbstractStaticKeysSdkCredentials(PlatformType platformType,
            Regex secretKeyPattern,
            Regex publicKeyPattern,
            string secretKey,
            string publicKey) : base(platformType)
        {
            ValidateSecretKey(secretKeyPattern, secretKey);
            ValidatePublicKey(publicKeyPattern, publicKey);
            SecretKey = secretKey;
            PublicKey = publicKey;
        }

        private static void ValidateSecretKey(Regex pattern, string key)
        {
            if (ValidKey(pattern, key))
            {
                return;
            }

            throw CheckoutArgumentException.WithMessage("invalid secret key");
        }

        private static void ValidatePublicKey(Regex pattern, string key)
        {
            // public key is not strictly mandatory
            if (key == null)
            {
                return;
            }

            if (ValidKey(pattern, key))
            {
                return;
            }

            throw CheckoutArgumentException.WithMessage("invalid public key");
        }

        private static bool ValidKey(Regex pattern, string key)
        {
            return pattern.IsMatch(key);
        }
    }
}