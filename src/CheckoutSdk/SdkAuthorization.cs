using System.Net.Http.Headers;

namespace Checkout
{
    public sealed class SdkAuthorization
    {
        private const string Bearer = "Bearer";

        private readonly PlatformType _platformType;
        private readonly string _credential;

        public SdkAuthorization(PlatformType platformType, string credential)
        {
            _platformType = platformType;
            _credential = credential;
        }

        public AuthenticationHeaderValue GetAuthorizationHeader()
        {
            switch (_platformType)
            {
                case PlatformType.Default:
                case PlatformType.Custom:
                    return new AuthenticationHeaderValue(_credential);
                case PlatformType.Four:
                case PlatformType.FourOAuth:
                    return new AuthenticationHeaderValue(Bearer, _credential);
                default:
                    throw new CheckoutAuthorizationException("Invalid platform type");
            }
        }

        private bool Equals(SdkAuthorization other)
        {
            return _platformType == other._platformType && _credential == other._credential;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SdkAuthorization other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) _platformType * 397) ^ (_credential != null ? _credential.GetHashCode() : 0);
            }
        }
    }
}