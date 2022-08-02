# Checkout.com .NET SDK

[![GitHub license](https://img.shields.io/github/license/checkout/checkout-sdk-net.svg)](https://github.com/checkout/checkout-sdk-net/blob/master/LICENSE)
[![build-master](https://github.com/checkout/checkout-sdk-net/actions/workflows/build-master.yml/badge.svg?branch=master)](https://github.com/checkout/checkout-sdk-net/actions/workflows/build-master.yml)
[![NuGet](https://img.shields.io/nuget/v/CheckoutSDK.svg)](https://www.nuget.org/packages/CheckoutSDK)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=checkout_checkout-sdk-net&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=checkout_checkout-sdk-net)

## Getting started

> **This is a Legacy Version** </br>
> We will provide support to some features or changes on APIs until users are fully migrated to the version 4.X.X,
> however we recommend to upgrade to version 4.X.X because this support is momentary and a lot of things were fixed and changed. </br>
> If you’re still having issues don't hesitate to open a [ticket](https://github.com/checkout/checkout-sdk-net/issues/new/choose). </br></br>
> Remember: </br>
> * In documentation Marketplace prefixes no longer exist, you need to search in documentation as Platforms. </br>
> * The `default` prefixes on versions < 4, in documentation you need to search as `previous` </br>
> * In documentation `Four` prefixes no longer exist, your need to search as `default`.

### :book: Checkout our official documentation.

* [Official Docs (Four)](https://docs.checkout.com/)
* [Official Docs (Previous/Current default)](https://docs.checkout.com/previous)

### :books: Check out our official API documentation guide, where you can also find more usage examples.

* [API Reference (Four)](https://api-reference.checkout.com/)
* [API Reference (Previous/Current default)](https://api-reference.checkout.com/previous)

Packages and sources are available from [Nuget](https://www.nuget.org/packages/CheckoutSDK).

## How to use the SDK

This SDK can be used with two different pair of API keys provided by Checkout. However, using different API keys imply using specific API features. Please find in the table below the types of keys that can be used within this SDK.

| Account System | Public Key (example)                    | Secret Key (example)                    |
| -------------- | --------------------------------------- | --------------------------------------- |
| Previous        | pk_g650ff27-7c42-4ce1-ae90-5691a188ee7b | sk_gk3517a8-3z01-45fq-b4bd-4282384b0a64 |
| Four           | pk_pkhpdtvabcf7hdgpwnbhw7r2uic          | sk_m73dzypy7cf3gf5d2xr4k7sxo4e          |

Note: Sandbox keys have a `test_` or `sbox_` identifier, for Default and Four accounts respectively.

**PLEASE NEVER SHARE OR PUBLISH YOUR CHECKOUT CREDENTIALS.**

### OAuth

It's only available for Four accounts and will only work for a limited number of features offered by the API. Please find below the instructions on how to set it up.

If you don't have your own API keys or OAuth access, you can start by signing up for a test account [here](https://www.checkout.com/get-test-account).

## Getting started

To get started install the [`CheckoutSDK`](https://www.nuget.org/packages/CheckoutSDK) package from NuGet.

Initialize a `CheckoutApi` to access the operations for each API. Please note that there are 2 different Checkout API interfaces, depending on the way the SDK is built.

### Previous

```c#
ICheckoutApi api = CheckoutSdk.DefaultSdk().StaticKeys()
    .PublicKey("public_key") // optional, only required for operations related with tokens
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .LogProvider(logFactory) // optional
    .HttpClientFactory(httpClientFactory) // optional
    .Build();
```

### Four

```c#
Four.ICheckoutApi api = CheckoutSdk.FourSdk().StaticKeys()
    .PublicKey("public_key") // optional, only required for operations related with tokens
    .SecretKey("secret_key")
    .Environment(Environment.Sandbox)
    .LogProvider(logFactory) // optional
    .HttpClientFactory(httpClientFactory) // optional
    .FilesEnvironment(Environment.Sandbox) // optional, needed for Marketplace operations
    .Build();
```
### Four OAuth

The SDK supports client credentials OAuth, when initialized as follows:

```c#
Four.ICheckoutApi api = CheckoutSdk.FourSdk().OAuth()
    .ClientCredentials("client_id", "client_secret")
    .AuthorizationUri(new Uri("https://access.sandbox.checkout.com/connect/token")) // custom authorization URI, optional
    .Scopes(FourOAuthScope.Files, FourOAuthScope.Flow) // array of scopes, optional
    .Environment(Environment.Sandbox)
    .LogProvider(logFactory) // optional
    .HttpClientFactory(httpClientFactory) // optional
    .FilesEnvironment(Environment.Sandbox) // optional, needed for Marketplace operations
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
    "PlatformType": "Default",
    "FilesEnvironment": "Sandbox"
  }
}
```
For OAuth, the configuration file should include the following properties:

```json
{
  "Checkout": {
    "ClientId": "client_id",
    "ClientSecret": "client_secret",
    "AuthorizationUri": "https://access.sandbox.checkout.com/connect/token",
    "Scopes": ["vault", "fx"],
    "Environment": "Sandbox",
    "PlatformType": "FourOAuth"
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

## Exception handling

All the API responses that do not fall in the 2** status codes will cause a `CheckoutApiException`. The exception encapsulates
the `requestId`, `httpStatusCode` and a map of `errorDetails`, if available.

## Building from source

Once you checkout the code from GitHub, the project can be built using the netcore CLI tools:

```
dotnet build

# run tests
dotnet test
```

The execution of integration tests require the following environment variables set in your system:

* For Previous account systems: `CHECKOUT_PUBLIC_KEY` & `CHECKOUT_SECRET_KEY`
* For Four account systems: `CHECKOUT_FOUR_PUBLIC_KEY` & `CHECKOUT_FOUR_SECRET_KEY`
* For Four account systems (OAuth): `CHECKOUT_FOUR_OAUTH_CLIENT_ID` & `CHECKOUT_FOUR_OAUTH_CLIENT_SECRET`

## Code of Conduct

Please refer to [Code of Conduct](CODE_OF_CONDUCT.md)

## Licensing

[MIT](LICENSE.md)