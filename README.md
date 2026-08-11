# Checkout.com .NET SDK

[![build-status](https://github.com/checkout/checkout-sdk-net/workflows/build-master/badge.svg)](https://github.com/checkout/checkout-sdk-net/actions/workflows/build-master.yml)
![CodeQL](https://github.com/checkout/checkout-sdk-net/workflows/CodeQL/badge.svg)

[![build-status](https://github.com/checkout/checkout-sdk-net/workflows/build-release/badge.svg)](https://github.com/checkout/checkout-sdk-net/actions/workflows/build-release.yml)
[![GitHub release](https://img.shields.io/github/release/checkout/checkout-sdk-net.svg)](https://GitHub.com/checkout/checkout-sdk-net/releases/)
[![NuGet](https://img.shields.io/nuget/v/CheckoutSDK.svg)](https://www.nuget.org/packages/CheckoutSDK)

[![GitHub license](https://img.shields.io/github/license/checkout/checkout-sdk-net.svg)](https://github.com/checkout/checkout-sdk-net/blob/master/LICENSE.md)

## Getting started 

> **Version 5.0.0 is here!**
>  <br/><br/>
> We've enhanced the SDK with .NET 8 support and comprehensive coverage of all current API modules. <br/>
> This version brings improved performance, better async patterns, and full compatibility with the latest .NET ecosystem. <br/>
> The SDK now includes complete support for all Checkout.com API modules:
> * **Payments & Processing**: Payments, PaymentMethods, Sources, Tokens, HandlePaymentsAndPayouts
> * **Customer Management**: Customers, Identities, Instruments, NetworkTokens
> * **Business Operations**: Accounts, Platforms, Workflows, Transfers, Balances
> * **Risk & Compliance**: Risk, Disputes, ComplianceRequests, StandaloneAccountUpdater
> * **Analytics & Reporting**: Reporting, Reconciliation, Events, Metadata
> * **Advanced Features**: Issuing, Forex, Forward, Files, Webhooks, Apm, AgenticCommerce
> * **Authentication & Authorization**: Authentication with full OAuth support
> 
> If you're upgrading from previous versions, most existing code will continue to work seamlessly. For any migration questions, don't hesitate to open a [ticket](https://github.com/checkout/checkout-sdk-net/issues/new/choose).

> **Version 4.0.0 is here!**
>  <br/><br/>
> We improved the initialization of SDK making it easier to understand the available options. <br/>
> Now `NAS` accounts are the default instance for the SDK and `ABC` structure was moved to a `previous` prefixes. <br/>
> If you have been using this SDK before, you may find the following important changes:
> * Marketplace module was moved to Accounts module, same for classes and references.
> * In most cases, IDE can help you determine from where to import, but if you’re still having issues don't hesitate to open a [ticket](https://github.com/checkout/checkout-sdk-net/issues/new/choose).

### :rocket: Please check in [GitHub releases](https://github.com/checkout/checkout-sdk-net/releases) for all the versions available.

### :book: Checkout our official documentation.

* [Official Docs (Default)](https://docs.checkout.com/)
* [Official Docs (Previous)](https://docs.checkout.com/previous)

### :books: Check out our official API documentation guide, where you can also find more usage examples.

* [API Reference (Default)](https://api-reference.checkout.com/)
* [API Reference (Previous)](https://api-reference.checkout.com/previous)

## How to use the SDK

This SDK can be used with two different pair of API keys provided by Checkout. However, using different API keys imply using specific API features. </br>
Please find in the table below the types of keys that can be used within this SDK.

| Account System | Public Key (example)                    | Secret Key (example)                    |
|----------------|-----------------------------------------| --------------------------------------- |
| Default        | pk_pkhpdtvabcf7hdgpwnbhw7r2uic          | sk_m73dzypy7cf3gf5d2xr4k7sxo4e          |
| Previous       | pk_g650ff27-7c42-4ce1-ae90-5691a188ee7b | sk_gk3517a8-3z01-45fq-b4bd-4282384b0a64 |

Note: sandbox keys have a `sbox_` or `test_` identifier, for Default and Previous accounts respectively.

**PLEASE NEVER SHARE OR PUBLISH YOUR CHECKOUT CREDENTIALS.**

If you don't have your own API keys, you can sign up for a test account [here](https://www.checkout.com/get-test-account).


### Subdomain value

Requests must be made through your merchant-specific subdomain (MSSD): the first 8 characters of your client ID (excluding `cli_`). For example, if your client ID is `cli_vkuhvk4vjn2edkps7dfsq6emqm`, your subdomain is `vkuhvk4v`. When the `EnvironmentSubdomain` is set the SDK will send requests to `https://vkuhvk4v.api.checkout.com`. See [Base URLs](https://api-reference.checkout.com/#section/Base-URLs) and [API endpoints](https://www.checkout.com/docs/developer-resources/api/api-endpoints) for further details, and instructions on where to find your unique client ID.

### Default

Default keys client instantiation can be done as follows:

```c#
ICheckoutApi api = CheckoutSdk.Builder().StaticKeys()
    .PublicKey("public_key") // optional, only required for operations related with tokens
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .EnvironmentSubdomain("subdomain") // required, Merchant-specific DNS name, the first 8 characters of your client ID
    .LogProvider(logFactory) // optional
    .HttpClientFactory(httpClientFactory) // optional
    .Build();
    
```

### Default OAuth

The SDK supports client credentials OAuth, when initialized as follows:

```c#
ICheckoutApi api = CheckoutSdk.Builder().OAuth()
    .ClientCredentials("client_id", "client_secret")
    .AuthorizationUri(new Uri("https://access.sandbox.checkout.com/connect/token")) // custom authorization URI, optional
    .Scopes(OAuthScope.Files, OAuthScope.Flow) // array of scopes, optional
    .Environment(Environment.Sandbox)
    .EnvironmentSubdomain("subdomain") // required, Merchant-specific DNS name, the first 8 characters of your client ID
    .LogProvider(logFactory) // optional
    .HttpClientFactory(httpClientFactory) // optional
    .Build();
```

### Previous

If your pair of keys matches the previous system type, this is how the SDK should be used:

```c#
Checkout.Previous.ICheckoutApi api = CheckoutSdk.Builder()
    .Previous()
    .StaticKeys()
    .PublicKey("public_key") // optional, only required for operations related with tokens
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .EnvironmentSubdomain("subdomain") // optional for the Previous platform, Merchant-specific DNS name
    .LogProvider(logFactory) // optional
    .HttpClientFactory(httpClientFactory) // optional
    .Build();
```

Then just get any client, and start making requests:

```c#
var paymentResponse = await api.PaymentsClient().RequestPayment(new PaymentRequest());
```

### .NET Core Applications

The [CheckoutSDK.Extensions.Microsoft](https://www.nuget.org/packages/CheckoutSDK.Extensions.Microsoft) package makes it easier to add the Checkout SDK to your .NET Core applications.

Initialize the Configuration of your `appsettings.json` file:

```json
{
  "Checkout": {
    "SecretKey": "secret_key",
    "PublicKey": "public_key",
    "Environment": "Sandbox",
    "EnvironmentSubdomain": "subdomain",
    "PlatformType": "Default" 
  }
}
```

`EnvironmentSubdomain` is required for the `Default` and `DefaultOAuth` platform types (see [Subdomain value](#subdomain-value)).
You can chose PlatformType `Default` or `Previous` depending of the type of keys and account system access.

For OAuth, the configuration file should include the following properties:

```json
{
  "Checkout": {
    "ClientId": "client_id",
    "ClientSecret": "client_secret",
    "AuthorizationUri": "https://access.sandbox.checkout.com/connect/token",
    "Scopes": ["vault", "fx"],
    "Environment": "Sandbox",
    "EnvironmentSubdomain": "subdomain",
    "PlatformType": "DefaultOAuth"
  }
}
```
Use `Environment` enum value to choose the desired environment for the SDK, and `PlatformType` value to choose between the different Account Settings. Then add the configuration:

```c#
public class Startup 
{
    public IConfigurationRoot Configuration { get; set; }

    public Startup(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        Configuration = builder.Build();
    }
}
```
Register the `CheckoutSdk`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    // LogFactory and HttpClientFactory are optional
    CheckoutServiceCollection.AddCheckoutSdk(services, configuration, logFactory, httpClientFactory);
}
```
Then take a dependency on `ICheckoutApi` in your class constructor:

```c#
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutApi _api;

    public CheckoutController(ICheckoutApi api)
    {
        _api = api;
    }
}
```
Please note again that there are 2 different `ICheckoutApi` interfaces, depending on the way the SDK is built.

## Custom HttpClient

Sometimes you need a custom thread pool, or any custom http property, so you can provide your own httpClient configuration as follows.

```c#
// Create a custom class from IHttpClientFactory
private class CustomClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient()
    {
        var handler = new HttpClientHandler();
        handler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;
        var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromSeconds(2);
        return httpClient;
    }
}

ICheckoutApi api = CheckoutSdk.Builder().StaticKeys()
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .EnvironmentSubdomain("subdomain") // required, Merchant-specific DNS name, first 8 characters of your client ID
    .HttpClientFactory(new CustomClientFactory()) // optional
    .Build();
```

## Logging

The SDK supports custom LogProvider that extends from Microsoft.Extensions.Logging `ILoggerFactory`, you need to provide your configuration as follows.

```c#
var logFactory = new NLogLoggerFactory();
_log = logFactory.CreateLogger(typeof(SandboxTestFixture));

ICheckoutApi api = CheckoutSdk.Builder().StaticKeys()
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .EnvironmentSubdomain("subdomain") // required, Merchant-specific DNS name, first 8 characters of your client ID
    .LogProvider(logFactory)
    .Build();
```

## Legacy domain (emergency use only)

> :warning: **Only use if merchant specific sub domains are causing issues** Connecting through your merchant-specific subdomain (see [Subdomain value](#subdomain-value)) is the supported way of using the Checkout.com API, and non-subdomain usage will be deprecated.

If, in exceptional circumstances, you cannot use your merchant-specific subdomain, you can explicitly opt out by calling `UseLegacyDomain()` instead of `EnvironmentSubdomain(...)`:

```c#
ICheckoutApi api = CheckoutSdk.Builder().StaticKeys()
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .UseLegacyDomain() // deprecated, emergency fallback only
    .Build();
```

This routes requests to `api.checkout.com` (or `api.sandbox.checkout.com`) and `access.checkout.com` (or `access.sandbox.checkout.com`). The method is marked as deprecated and produces a compile-time warning. Exactly one of `EnvironmentSubdomain(...)` or `UseLegacyDomain()` must be set — the SDK will fail to build the client if both, or neither, are set. When using the `CheckoutSDK.Extensions.Microsoft` package, the equivalent configuration property is `"UseLegacyDomain": true`.

## Running the tests against your subdomain

The test suite builds every client through `TestDomainConfiguration`, which has two modes. By default it uses the shared hosts, because the sandbox OAuth clients are not provisioned for merchant-specific subdomains and the token request would come back `invalid_client`. To run against a subdomain instead:

```bash
export CHECKOUT_MERCHANT_SUBDOMAIN="your_subdomain"
export CHECKOUT_TEST_USE_SUBDOMAIN=true
dotnet test
```

The switch is separate from `CHECKOUT_MERCHANT_SUBDOMAIN` on purpose: CI already exports that secret, so provisioning is what should flip the behaviour, not the presence of a value. Once sandbox is provisioned like production, set `CHECKOUT_TEST_USE_SUBDOMAIN: 'true'` in the workflows and CI exercises the subdomain path end to end.

## Exception handling

All the API responses that do not fall in the 2** status codes will cause a `CheckoutApiException`. The exception encapsulates
the `requestId`, `httpStatusCode` and a map of `errorDetails`, if available.

## Building from source

Once you check out the code from GitHub, the project can be built using the netcore CLI tools:

```
dotnet build

# run tests
dotnet test
```

The execution of integration tests require the following environment variables set in your system:

* For Default account systems (NAS): `CHECKOUT_DEFAULT_PUBLIC_KEY` & `CHECKOUT_DEFAULT_SECRET_KEY`
* For Default account systems (OAuth): `CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` & `CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET`
* For Previous account systems (ABC): `CHECKOUT_PREVIOUS_PUBLIC_KEY` & `CHECKOUT_PREVIOUS_SECRET_KEY`

## Telemetry
Request telemetry is enabled by default in the .NET SDK. Request latency is included in the telemetry data. Recording the request latency allows Checkout.com to continuously monitor and improve the merchant experience.

Request telemetry can be disabled by opting out during CheckoutSdk builder step:
```
ICheckoutApi api = CheckoutSdk.Builder().StaticKeys()
    .SecretKey("secret_key")
    .RecordTelemetry(false)
    .Environment(Environment.Sandbox)
    .Build();
```

Or when using `CheckoutSDK.Extensions.Microsoft`:
```
{
  "Checkout": {
    ...
    "RecordTelemetry": false,
    ...
  }
}
```

## Code of Conduct

Please refer to [Code of Conduct](CODE_OF_CONDUCT.md)

## Licensing

[MIT](LICENSE.md)