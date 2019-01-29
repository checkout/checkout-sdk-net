# Checkout.com Sample Application

## Running the sample

First update `appsettings.json` to include your Checkout.com API keys or add an `appsettings.local.json` file with your settings:

 ```json
  "Checkout": {
    "UseSandbox": true,
    "SecretKey": "your_secret_key",
    "PublicKey": "your_public_key"
  }
```

Install CSS/JS dependencies using Bower (should be done automatically if using Visual Studio 2017):

```
bower install
```

Run the application from the editor of your choice or via command line:

```
dotnet run
```

The sample application will be running on http://localhost:5000.