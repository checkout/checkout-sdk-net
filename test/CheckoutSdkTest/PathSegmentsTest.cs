using Checkout.Payments;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout
{
    /// <summary>
    /// Every client method sends each string argument that ends up in the request path as exactly one
    /// encoded path segment, and refuses values that cannot be one segment before sending.
    /// </summary>
    public class PathSegmentsTest
    {
        private const string EncodedValue = "x_1?y=z#w%v +\u00e9";

        private const string InvalidPathSegmentMessage = "The value is not a valid path segment";

        public static IEnumerable<object[]> EncodedCases() => Cases(EncodedValue);

        public static IEnumerable<object[]> RefusedCases() =>
            Cases("..").Concat(Cases(".")).Concat(Cases("x_1/y")).Concat(Cases("../x_1")).Concat(Cases("x_1\\y"))
                .Concat(Cases("x_1\r\ny")).Concat(Cases("x_1\0")).Concat(Cases("x_1\ud800"));

        [Theory]
        [MemberData(nameof(EncodedCases))]
        public async Task ShouldSendEachStringArgumentAsOneEncodedSegment(string client, string method,
            int parameter, string value)
        {
            var paths = await InvokeRecording(client, method, parameter, value);

            foreach (var path in paths.Where(p => p.Contains("x_1")))
            {
                path.ShouldContain(Uri.EscapeDataString(value));
                path.ShouldNotContain("#");
                path.ShouldNotContain(value);
            }
        }

        [Theory]
        [MemberData(nameof(RefusedCases))]
        public async Task ShouldRefuseValuesThatCannotBeOneSegment(string client, string method, int parameter,
            string value)
        {
            var paths = await InvokeRecording(client, method, parameter, value);

            foreach (var path in paths)
            {
                var pathOnly = path.Split('?')[0];
                pathOnly.ShouldNotContain("x_1");
                pathOnly.Split('/', '\\').ShouldNotContain("..");
                pathOnly.Split('/', '\\').ShouldNotContain(".");
            }
        }

        [Fact]
        public void ShouldCoverEveryClientOfThePublicApi()
        {
            var clientTypes = ClientTypes().ToList();
            var apiClients = new[] { typeof(ICheckoutApi), typeof(Previous.ICheckoutApi) }
                .SelectMany(api => new[] { api }.Concat(api.GetInterfaces()))
                .SelectMany(api => api.GetMethods())
                .Select(m => m.ReturnType)
                .Where(t => t.IsInterface && t.Name.EndsWith("Client"))
                .Distinct()
                .ToList();

            apiClients.ShouldNotBeEmpty();
            foreach (var apiClient in apiClients)
            {
                clientTypes.ShouldContain(t => apiClient.IsAssignableFrom(t), apiClient.FullName);
            }
        }

        [Fact]
        public async Task ShouldSendEveryIdArgumentInTheRequest()
        {
            var idArguments = Cases("x_marker")
                .Where(c => IsIdParameter(ParameterName((string)c[0], (string)c[1], (int)c[2])))
                .ToList();
            idArguments.ShouldNotBeEmpty();

            foreach (var c in idArguments)
            {
                var type = typeof(AbstractClient).Assembly.GetType((string)c[0]);
                var method = ClientMethods(type).Single(m => m.ToString() == (string)c[1]);
                var apiClient = new RecordingApiClient();
                await InvokeWithSamples(type, method, (int)c[2], "x_marker", apiClient);
                apiClient.Paths.Concat(apiClient.Queries).ShouldContain(p => p.Contains("x_marker"),
                    $"{c[0]}.{c[1]} parameter {ParameterName((string)c[0], (string)c[1], (int)c[2])}");
            }
        }

        [Fact]
        public async Task ShouldBuildEveryRequestPathFromValidArguments()
        {
            var recorded = 0;
            foreach (var type in ClientTypes())
            {
                foreach (var method in ClientMethods(type))
                {
                    var (paths, exception) = await InvokeWithSamples(type, method, -1, null);
                    var refused = exception as CheckoutArgumentException;
                    (refused?.Message == InvalidPathSegmentMessage).ShouldBeFalse($"{type.FullName}.{method}");
                    recorded += paths.Count;
                }
            }

            recorded.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task ShouldEncodePathSegmentsOnTheWire()
        {
            var handler = new RecordingHandler();
            var paymentsClient = new PaymentsClient(WireApiClient(handler), Configuration());

            await paymentsClient.VoidPayment("pay_x?y=z#w");

            handler.Requests.Single().RequestUri.AbsolutePath.ShouldBe("/payments/pay_x%3Fy%3Dz%23w/voids");
            handler.Requests.Single().RequestUri.Query.ShouldBeEmpty();
            handler.Requests.Single().RequestUri.Fragment.ShouldBeEmpty();
        }

        [Fact]
        public async Task ShouldKeepValidIdsUnchangedOnTheWire()
        {
            var handler = new RecordingHandler();
            var paymentsClient = new PaymentsClient(WireApiClient(handler), Configuration());

            await paymentsClient.VoidPayment("pay_abcdefghijklmnopqrstuvwxyz");

            handler.Requests.Single().RequestUri.AbsoluteUri
                .ShouldBe("https://api.sandbox.checkout.com/payments/pay_abcdefghijklmnopqrstuvwxyz/voids");
        }

        [Theory]
        [InlineData("..")]
        [InlineData(".")]
        [InlineData("../x_2")]
        [InlineData("pay_x/y")]
        [InlineData("pay_x\\y")]
        [InlineData("pay_x\r\n")]
        public async Task ShouldRefuseInvalidSegmentsBeforeSending(string paymentId)
        {
            var handler = new RecordingHandler();
            var paymentsClient = new PaymentsClient(WireApiClient(handler), Configuration());

            await Assert.ThrowsAsync<CheckoutArgumentException>(() => paymentsClient.GetPaymentDetails(paymentId));

            handler.Requests.ShouldBeEmpty();
        }

        [Fact]
        public async Task ShouldEncodeEventTypesVersionOnTheWire()
        {
            var handler = new RecordingHandler("[]");
            var eventsClient = new Events.Previous.EventsClient(WireApiClient(handler), Configuration());

            await eventsClient.RetrieveAllEventTypes("1.0&x=y#z");

            var uri = handler.Requests.Single().RequestUri;
            uri.AbsolutePath.ShouldBe("/event-types");
            uri.Query.ShouldBe("?version=1.0%26x%3dy%23z");
            uri.Fragment.ShouldBeEmpty();
        }

        [Fact]
        public async Task ShouldKeepEventTypesVersionUnchangedOnTheWire()
        {
            var handler = new RecordingHandler("[]");
            var eventsClient = new Events.Previous.EventsClient(WireApiClient(handler), Configuration());

            await eventsClient.RetrieveAllEventTypes("1.0");

            handler.Requests.Single().RequestUri.PathAndQuery.ShouldBe("/event-types?version=1.0");
        }

        private static IEnumerable<object[]> Cases(string value)
        {
            foreach (var type in ClientTypes())
            {
                foreach (var method in ClientMethods(type))
                {
                    var parameters = method.GetParameters();
                    for (var i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].ParameterType == typeof(string))
                        {
                            yield return new object[] { type.FullName, method.ToString(), i, value };
                        }
                    }
                }
            }
        }

        private static IEnumerable<Type> ClientTypes() =>
            typeof(AbstractClient).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && typeof(AbstractClient).IsAssignableFrom(t))
                .OrderBy(t => t.FullName);

        private static IEnumerable<MethodInfo> ClientMethods(Type type) =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => typeof(Task).IsAssignableFrom(m.ReturnType) && !m.IsGenericMethodDefinition)
                .OrderBy(m => m.ToString());

        private static async Task<IList<string>> InvokeRecording(string clientName, string methodName,
            int parameter, string value)
        {
            var type = typeof(AbstractClient).Assembly.GetType(clientName);
            var method = ClientMethods(type).Single(m => m.ToString() == methodName);
            var (paths, _) = await InvokeWithSamples(type, method, parameter, value);
            return paths;
        }

        private static async Task<(IList<string> Paths, Exception Exception)> InvokeWithSamples(Type type,
            MethodInfo method, int parameter, string value, RecordingApiClient apiClient = null)
        {
            apiClient = apiClient ?? new RecordingApiClient();
            var client = CreateClient(type, apiClient);

            var arguments = method.GetParameters()
                .Select((p, i) => i == parameter ? value : SampleArgument(p))
                .ToArray();
            try
            {
                await (Task)method.Invoke(client, arguments);
            }
            catch (TargetInvocationException e) when (!(e.InnerException is StopException))
            {
                // refused before sending, or the sample arguments were not enough to reach the request
                return (apiClient.Paths, e.InnerException);
            }
            catch (StopException)
            {
                // request recorded
            }
            catch (TargetInvocationException)
            {
                // request recorded (thrown synchronously)
            }
            catch (Exception e)
            {
                // refused before sending (async methods), or the sample arguments were not enough
                return (apiClient.Paths, e);
            }

            return (apiClient.Paths, null);
        }

        private static string ParameterName(string clientName, string methodName, int parameter)
        {
            var type = typeof(AbstractClient).Assembly.GetType(clientName);
            return ClientMethods(type).Single(m => m.ToString() == methodName).GetParameters()[parameter].Name;
        }

        // Resource identifiers by naming convention: "id" or a name ending in "Id".
        private static bool IsIdParameter(string name) => name == "id" || name.EndsWith("Id");

        private static object CreateClient(Type type, IApiClient apiClient)
        {
            var constructor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();
            var arguments = constructor.GetParameters().Select(p =>
            {
                if (p.ParameterType == typeof(IApiClient)) return apiClient;
                if (p.ParameterType == typeof(CheckoutConfiguration)) return (object)Configuration();
                return p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null;
            }).ToArray();
            return constructor.Invoke(arguments);
        }

        private static object SampleArgument(ParameterInfo parameter)
        {
            var type = parameter.ParameterType;
            if (type == typeof(string)) return "x_valid";
            if (type == typeof(CancellationToken)) return CancellationToken.None;
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0);
            if (type.IsValueType) return Activator.CreateInstance(type);
            if (type.IsArray) return Array.CreateInstance(type.GetElementType(), 0);
            var concrete = new[] { type }
                .Concat(type.Assembly.GetTypes().Where(t => t.IsPublic && type.IsAssignableFrom(t)).OrderBy(t => t.FullName))
                .FirstOrDefault(t => !t.IsAbstract && !t.IsInterface && t.GetConstructor(Type.EmptyTypes) != null);
            return concrete == null ? null : Activator.CreateInstance(concrete);
        }

        private static CheckoutConfiguration Configuration()
        {
            var credentials = new Mock<SdkCredentials>(PlatformType.Default);
            credentials.Setup(c => c.GetSdkAuthorization(It.IsAny<SdkAuthorizationType>()))
                .Returns(new SdkAuthorization(PlatformType.Default, "sk_sbox_x"));
            return new CheckoutConfiguration(credentials.Object, Environment.Sandbox,
                new Mock<IHttpClientFactory>().Object);
        }

        private static IApiClient WireApiClient(HttpMessageHandler handler)
        {
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient()).Returns(() => new HttpClient(handler));
            return new ApiClient(factory.Object, new Uri("https://api.sandbox.checkout.com/"), false);
        }

        private sealed class StopException : Exception
        {
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            private readonly string _body;

            public RecordingHandler(string body = "{}")
            {
                _body = body;
            }

            public List<HttpRequestMessage> Requests { get; } = new List<HttpRequestMessage>();

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                Requests.Add(request);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_body, Encoding.UTF8, "application/json")
                });
            }
        }

        private sealed class RecordingApiClient : IApiClient
        {
            public List<string> Paths { get; } = new List<string>();

            // Query filters, serialized as the SDK sends them.
            public List<string> Queries { get; } = new List<string>();

            private Task<TResult> Record<TResult>(string path)
            {
                Paths.Add(path);
                throw new StopException();
            }

            public Task<TResult> Get<TResult>(string path, SdkAuthorization authorization,
                CancellationToken cancellationToken = default) where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Get<TResult>(string path, SdkAuthorization authorization, IHeaders headers,
                CancellationToken cancellationToken = default) where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Post<TResult>(string path, SdkAuthorization authorization, object request = null,
                CancellationToken cancellationToken = default, string idempotencyKey = null)
                where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Post<TResult>(string path, SdkAuthorization authorization,
                IDictionary<int, Type> resultTypeMappings, object request = null,
                CancellationToken cancellationToken = default, string idempotencyKey = null)
                where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Post<TResult>(string path, SdkAuthorization authorization, object request,
                CancellationToken cancellationToken, string idempotencyKey, IHeaders headers)
                where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Patch<TResult>(string path, SdkAuthorization authorization, object request = null,
                CancellationToken cancellationToken = default, string idempotencyKey = null, IHeaders headers = null)
                where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Put<TResult>(string path, SdkAuthorization authorization, object request = null,
                CancellationToken cancellationToken = default, string idempotencyKey = null, IHeaders headers = null)
                where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Delete<TResult>(string path, SdkAuthorization authorization,
                CancellationToken cancellationToken = default) where TResult : HttpMetadata => Record<TResult>(path);

            public Task<TResult> Query<TResult>(string path, SdkAuthorization authorization, object request = null,
                CancellationToken cancellationToken = default) where TResult : HttpMetadata
            {
                if (request != null)
                {
                    Queries.Add(new JsonSerializer().Serialize(request));
                }

                return Record<TResult>(path);
            }
        }
    }
}
