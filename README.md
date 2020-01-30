# Checkout SDK for .NET

[![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/6ox0xlfjv11avkdf?svg=true)](https://ci.appveyor.com/project/checkout/checkout-sdk-net-74764)
[![NuGet](https://img.shields.io/nuget/v/CheckoutSDK.svg)](https://www.nuget.org/packages/CheckoutSDK)
[![MyGet Pre Release](https://img.shields.io/myget/checkout/vpre/CheckoutSDK.svg)](https://www.myget.org/feed/checkout/package/nuget/CheckoutSDK)

The **Checkout SDK for .NET** enables .NET developers to easily work with [Checkout.com APIs](https://docs.checkout.com/). 
It supports .NET Framework 4.5+ and .NET Core.

## Getting Help

If you encounter a bug with Checkout SDK for .NET plase search the existing issues and try to make sure your problem doesn’t already exist before opening a new issue. It's helpful if you include the version of Checkout SDK .NET and the OS you're using. Please include a stack trace and reduced repro case when appropriate, too.

The GitHub issues are intended for bug reports and feature requests. For help and questions with using Checkout SDK for .NET please contact our integration support team.

For full usage details, see the [Wiki](https://github.com/checkout/checkout-sdk-net/wiki).

## Quickstart

To get started install the [`CheckoutSDK`](https://www.nuget.org/packages/CheckoutSDK) package from NuGet. 

Initialize a `CheckoutApi` to access the operations for each API:

```c#
var api = CheckoutApi.Create("sk_70d144d5-92bd-4040-83cf-faeb978b3d75", useSandbox: true);

var paymentRequest = new PaymentRequest<TokenSource>(new TokenSource("tok_ubfj2q76miwundwlk72vxt2i7q"), Currency.USD, 999);
var apiResponse = await api.Payments.RequestAsync(paymentRequest);
```

All API operations return an `ApiResponse<TResult>` where `TResult` contains the result of the API call, as per our [API reference](https://docs.checkout.com/reference).

For detailed examples, please see the WIKI.

### .NET Core Applications

The [`CheckoutSDK.Extensions.Microsoft`](https://www.nuget.org/packages/CheckoutSDK.Extensions.Microsoft) package makes it easy to add the Checkout SDK to your .NET Core applications.

Once installed register the SDK with the built-in DI container in `Startup.cs`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    // ...
    var configuration = new CheckoutConfiguration("sk_70d144d5-92bd-4040-83cf-faeb978b3d75", useSandbox: true);
    services.AddCheckoutSdk(configuration);    
}
```

Then take a dependency on `ICheckoutApi` in your class constructor:

```c#
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutApi _checkoutApi;

    public CheckoutController(ICheckoutApi checkoutApi)
    {
        _checkoutApi = checkoutApi ?? throw new ArgumentNullException(nameof(checkoutApi));
    }

    // etc.
}
```

To configure the SDK using [.NET Core Configuration](https://github.com/aspnet/Configuration) pass your application's `IConfiguration`:

```c#
public class Startup
{
    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // ...
        services.AddCheckoutSdk(Configuration);    
    }
}
```

You can then configure `appsettings.json` file with your Checkout details:

```json
{
  "Checkout": {
    "UseSandbox": true,
    "SecretKey" : "sk_70d144d5-92bd-4040-83cf-faeb978b3d75"
  }
}
```

For more details on configuring the SDK, see the [Wiki](https://github.com/checkout/checkout-sdk-net/wiki).

## Building and running tests

To build the project and run the integration tests, run `build.sh` (Mac/Unix) or `build.ps1` (Windows). The integration tests require your Sandbox keys to be configured. You can do this by adding `test/CheckoutSdk.Tests/appsettings.local.json` with your keys or setting the following environment variables:

- `Checkout__SecretKey`
- `Checkout__PublicKey`

## Versioning

Checkout SDK for .NET uses [Semantic Versioning](https://semver.org/). The latest stable code can be found on the `master` branch and will be published to NuGet. Unstable builds can be downloaded from the [Checkout MyGet server](https://www.myget.org/feed/Packages/checkout).

## More Resources

- [Checkout.com Documentation](http://docs.checkout.com)
- [Checkout.com API Reference](http://docs.checkout.com/reference)


## Release Process

- Create a PR from `develop` to `master` "Version {Version}" (do not squash merge)
- Pull the latest from `master` and tag `git tag -a {Version} -m "Version {Version}"`
- Push the tag: `git push origin master --tags` (this will deploy the package to NuGet)
- Create a PR from `master` to `develop` to bump the next version (do not squash merge)
