# Sample application

## How to set up

To set up the included sample application all you need to do is update `appsettings.json` file in `samples\CheckoutSdk.SampleApp` folder with your credentials:

```json
  "Checkout": {
    "UseSandbox": true,
    "SecretKey": "your_secret_key",
    "PublicKey": "your_public_key"
  }
```

*You may also create your own `appsettings.local.json` file to be used locally.*

The project can now be started from `checkout-sdk-net` solution. 

Bower dependencies should be downloaded automatically to `wwwroot\lib` as you open the solution for the first time in Visual Studio 2017.