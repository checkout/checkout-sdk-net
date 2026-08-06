namespace Checkout.Previous
{
    public class CheckoutPreviousSdk
    {
        public CheckoutStaticKeysSdkBuilder StaticKeys()
        {
            return new CheckoutStaticKeysSdkBuilder();
        }

        public class CheckoutStaticKeysSdkBuilder : AbstractCheckoutSdkBuilder<ICheckoutApi>
        {
            private string _publicKey;
            private string _secretKey;

            // The Previous (ABC) platform predates merchant-specific subdomains, so it is exempt
            // from the mandatory EnvironmentSubdomain/UseLegacyDomain configuration.
            protected override bool RequiresEnvironmentSubdomain => false;

            public CheckoutStaticKeysSdkBuilder PublicKey(string publicKey)
            {
                _publicKey = publicKey;
                return this;
            }

            public CheckoutStaticKeysSdkBuilder SecretKey(string secretKey)
            {
                _secretKey = secretKey;
                return this;
            }

            protected override SdkCredentials GetSdkCredentials()
            {
                return new PreviousStaticKeysSdkCredentials(_secretKey, _publicKey);
            }

            public override ICheckoutApi Build()
            {
                return new CheckoutApi(GetCheckoutConfiguration());
            }
        }
    }
}