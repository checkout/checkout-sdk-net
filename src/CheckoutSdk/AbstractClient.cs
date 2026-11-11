using System;

namespace Checkout
{
    public abstract class AbstractClient
    {
        private const string InvalidPathSegmentMessage = "The value is not a valid path segment";

        protected readonly IApiClient ApiClient;
        private readonly CheckoutConfiguration _configuration;
        private readonly SdkAuthorizationType _sdkAuthorizationType;

        protected AbstractClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration,
            SdkAuthorizationType sdkAuthorizationType)
        {
            ApiClient = apiClient;
            _configuration = configuration;
            _sdkAuthorizationType = sdkAuthorizationType;
        }


        protected SdkAuthorization SdkAuthorization()
        {
            return _configuration.SdkCredentials.GetSdkAuthorization(_sdkAuthorizationType);
        }

        protected SdkAuthorization SdkAuthorization(SdkAuthorizationType sdkAuthorizationType)
        {
            return _configuration.SdkCredentials.GetSdkAuthorization(sdkAuthorizationType);
        }

        /// <summary>
        /// Builds a relative request path from its segments, path constants and caller values alike. Each segment is
        /// percent-encoded on its own (RFC 3986: letters, digits, <c>-</c>, <c>.</c>, <c>_</c> and <c>~</c> are kept),
        /// so characters such as <c>?</c>, <c>#</c>, <c>%</c> and spaces stay inside their segment. Pass values
        /// unencoded: an already encoded value is encoded again.
        /// </summary>
        /// <param name="pathParams">The path segments, in order; each one is a single segment without <c>/</c>.</param>
        /// <returns>The encoded segments joined with <c>/</c>.</returns>
        /// <exception cref="CheckoutArgumentException">A segment is null, empty, <c>.</c> or <c>..</c>, contains
        /// <c>/</c>, <c>\</c> or an ASCII control character (U+0000 to U+001F, U+007F), or contains an unpaired
        /// surrogate.</exception>
        protected static string BuildPath(params string[] pathParams)
        {
            if (pathParams == null)
            {
                throw CheckoutArgumentException.WithMessage(InvalidPathSegmentMessage);
            }

            var segments = new string[pathParams.Length];
            for (var i = 0; i < pathParams.Length; i++)
            {
                segments[i] = EncodePathSegment(pathParams[i]);
            }

            return string.Join("/", segments);
        }

        protected bool IsSandbox()
        {
            return _configuration.Environment.Equals(Environment.Sandbox);
        }

        private static string EncodePathSegment(string value)
        {
            if (string.IsNullOrEmpty(value) || value == "." || value == "..")
            {
                throw CheckoutArgumentException.WithMessage(InvalidPathSegmentMessage);
            }

            for (var i = 0; i < value.Length; i++)
            {
                var c = value[i];
                if (c == '/' || c == '\\' || c < 0x20 || c == 0x7F)
                {
                    throw CheckoutArgumentException.WithMessage(InvalidPathSegmentMessage);
                }

                if (char.IsHighSurrogate(c) && i + 1 < value.Length && char.IsLowSurrogate(value[i + 1]))
                {
                    i++;
                }
                else if (char.IsSurrogate(c))
                {
                    throw CheckoutArgumentException.WithMessage(InvalidPathSegmentMessage);
                }
            }

            try
            {
                return Uri.EscapeDataString(value);
            }
            catch (UriFormatException)
            {
                throw CheckoutArgumentException.WithMessage(InvalidPathSegmentMessage);
            }
        }
    }
}
