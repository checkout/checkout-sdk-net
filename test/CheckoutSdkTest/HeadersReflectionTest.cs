using Checkout.Accounts;
using Checkout.AgenticCommerce.Requests;
using Checkout.Authentication;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests;
using Newtonsoft.Json;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Checkout
{
    public class HeadersReflectionTest
    {
        [Fact]
        public void DelegatedPaymentHeaders_ShouldResolveAllHeaderNames()
        {
            var headers = new DelegatedPaymentHeaders
            {
                Signature = "abc123==",
                Timestamp = "2026-03-11T10:30:00Z",
                ApiVersion = "2024-10-01"
            };

            var resolved = ResolveHeaders(headers);

            resolved.ShouldContainKey("Signature");
            resolved["Signature"].ShouldBe("abc123==");

            resolved.ShouldContainKey("Timestamp");
            resolved["Timestamp"].ShouldBe("2026-03-11T10:30:00Z");

            resolved.ShouldContainKey("API-Version");
            resolved["API-Version"].ShouldBe("2024-10-01");

            resolved.Count.ShouldBe(3);
        }

        [Fact]
        public void DelegatedPaymentHeaders_ShouldSkipNullAndEmptyValues()
        {
            var headers = new DelegatedPaymentHeaders
            {
                Signature = "abc123==",
                Timestamp = null,
                ApiVersion = ""
            };

            var resolved = ResolveHeaders(headers);

            resolved.ShouldContainKey("Signature");
            resolved.Count.ShouldBe(1);
        }

        [Fact]
        public void AccountsHeaders_ShouldResolveIfMatch()
        {
            var headers = new Headers
            {
                IfMatch = "etag-value-123"
            };

            var resolved = ResolveHeaders(headers);

            resolved.ShouldContainKey("if-match");
            resolved["if-match"].ShouldBe("etag-value-123");
            resolved.Count.ShouldBe(1);
        }

        [Fact]
        public void AccountsHeaders_ShouldSkipNullIfMatch()
        {
            var headers = new Headers
            {
                IfMatch = null
            };

            var resolved = ResolveHeaders(headers);

            resolved.Count.ShouldBe(0);
        }

        [Fact]
        public void SessionHeaders_ShouldResolveChannel()
        {
            var headers = new SessionHeaders
            {
                Channel = ChannelType.Browser
            };

            var resolved = ResolveHeaders(headers);

            resolved.ShouldContainKey("channel");
            resolved["channel"].ShouldBe("Browser");
            resolved.Count.ShouldBe(1);
        }

        [Fact]
        public void SessionHeaders_ShouldSkipNullChannel()
        {
            var headers = new SessionHeaders
            {
                Channel = null
            };

            var resolved = ResolveHeaders(headers);

            resolved.Count.ShouldBe(0);
        }

        [Fact]
        public void AllIHeadersImplementations_ShouldHaveJsonPropertyOnEveryProperty()
        {
            var headerTypes = typeof(IHeaders).Assembly
                .GetTypes()
                .Where(t => typeof(IHeaders).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var headerType in headerTypes)
            {
                foreach (var prop in headerType.GetProperties())
                {
                    var attr = prop.GetCustomAttribute<JsonPropertyAttribute>();
                    attr.ShouldNotBeNull(
                        $"{headerType.Name}.{prop.Name} is missing [JsonProperty] — " +
                        "ApiClient uses it to resolve the HTTP header name");
                    attr.PropertyName.ShouldNotBeNullOrWhiteSpace(
                        $"{headerType.Name}.{prop.Name} has [JsonProperty] but PropertyName is empty");
                }
            }
        }

        /// <summary>
        /// Mirrors the reflection logic in ApiClient.Invoke for resolving IHeaders to HTTP headers.
        /// </summary>
        private static Dictionary<string, string> ResolveHeaders(IHeaders headers)
        {
            var result = new Dictionary<string, string>();
            foreach (var property in headers.GetType().GetProperties())
            {
                var value = property.GetValue(headers)?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    var jsonAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
                    var headerName = jsonAttr?.PropertyName ?? property.Name;
                    result[headerName] = value;
                }
            }

            return result;
        }
    }
}
