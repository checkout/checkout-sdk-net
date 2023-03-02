using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Destination;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Request.Source.Apm;
using Checkout.Payments.Response;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Product = Checkout.Payments.Request.Product;

namespace Checkout.Payments
{
    public class PaymentsClientTest : UnitTestFixture
    {
        private const string PaymentsPath = "payments";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public PaymentsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldRequestPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestProviderTokenSource
                {
                    Token = "token", PaymentMethod = "method", AccountHolder = new AccountHolder()
                }
            };
            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayment(paymentRequest, null, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldRequestPayment_CustomSource()
        {
            var paymentRequest = new PaymentRequest
            {
                Customer = new CustomerRequest {Phone = new Phone()},
                Processing =
                    new ProcessingSettings
                    {
                        TaxAmount = 500,
                        ShippingAmount = 1000,
                    },
                Source = new RequestTamaraSource
                {
                    BillingAddress = new Address
                    {
                        AddressLine1 = "Cecilia Chapman",
                        AddressLine2 = "711-2880 Nulla St.",
                        City = "Mankato",
                        State = "Mississippi",
                        Zip = "96522",
                        Country = CountryCode.SA
                    }
                },
                Items = new List<Product>
                {
                    new Product
                    {
                        Name = "Item name",
                        Quantity = 3,
                        UnitPrice = 100,
                        TotalAmount = 100,
                        TaxAmount = 19,
                        DiscountAmount = 2,
                        Reference = "some description about item",
                        ImageUrl = "https://some_s3bucket.com",
                        Url = "https://some.website.com/item",
                        Sku = "123687000111"
                    }
                }
            };
            var paymentResponse = new PaymentResponse
            {
                Customer = new CustomerResponse {Id = "id", Email = "email", Name = "name", Phone = new Phone()},
                Processing = new PaymentProcessing {PartnerPaymentId = "123456"}
            };
            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayment(paymentRequest, null, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
            response.Customer.ShouldNotBeNull();
            response.Customer.Phone.ShouldNotBeNull();
        }
        
        [Fact]
        private async Task ShouldRequestPayment_IdempotencyKey()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestNetworkTokenSource
                {
                    Token = "token",
                    Cryptogram = "cryptogram",
                    Cvv = "123",
                    Eci = "eci",
                    Name = "name",
                    Phone = new Phone(),
                    Stored = false,
                    BillingAddress = new Address(),
                    ExpiryMonth = 12,
                    ExpiryYear = 2024,
                    TokenType = NetworkTokenType.Vts
                }
            };
            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayment(paymentRequest, "test", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldRequestPayout()
        {
            var payoutRequest = new PayoutRequest
            {
                Destination = new PaymentRequestBankAccountDestination
                {
                    AccountType = AccountType.Cash,
                    AccountNumber = "13654567455",
                    BankCode = "bank_code",
                    BranchCode = "6443",
                    Bban = "3704 0044 0532 0130 00",
                    Iban = "HU93116000060000000012345676",
                    SwiftBic = "37040044",
                    Country = CountryCode.HU,
                    AccountHolder = new AccountHolder {FirstName = "First Name", LastName = "Last Name",},
                    Bank = new BankDetails {Name = "Bank Name", Address = new Address(), Branch = "branch"}
                }
            };
            var payoutResponse = new PayoutResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PayoutResponse>(PaymentsPath, _authorization, payoutRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => payoutResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayout(payoutRequest, null, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(payoutResponse);
        }

        [Fact]
        private async Task ShouldRequestPayout_IdempotencyKey()
        {
            var payoutRequest = new PayoutRequest();
            var payoutResponse = new PayoutResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PayoutResponse>(PaymentsPath, _authorization, payoutRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => payoutResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayout(payoutRequest, "test", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(payoutResponse);
        }

        [Fact]
        private async Task ShouldGetPaymentsList()
        {
            var query = new PaymentsQueryFilter();
            var responsePayments = new PaymentsQueryResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<PaymentsQueryResponse>(PaymentsPath, _authorization, query, CancellationToken.None))
                .ReturnsAsync(() => responsePayments);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.GetPaymentsList(query, CancellationToken.None);
            
            response.ShouldNotBeNull();
            response.ShouldBeSameAs(responsePayments);
        }

        [Fact]
        private async Task ShouldGetPaymentDetails()
        {
            var paymentResponse = new GetPaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetPaymentResponse>(PaymentsPath + "/payment_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.GetPaymentDetails("payment_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldGetPaymentActions()
        {
            var paymentActions = new ItemsResponse<PaymentAction>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<PaymentAction>>(PaymentsPath + "/payment_id/actions", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => paymentActions);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.GetPaymentActions("payment_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentActions);
        }

        [Fact]
        private async Task ShouldCapturePayment_Id()
        {
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>(PaymentsPath + "/payment_id/captures", _authorization,
                        null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => captureResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.CapturePayment("payment_id", null);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCapturePayment_IdempotencyKey()
        {
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>(PaymentsPath + "/payment_id/captures", _authorization,
                        null,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => captureResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.CapturePayment("payment_id", null, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCapturePayment_Request()
        {
            var captureRequest = new CaptureRequest();
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>(PaymentsPath + "/payment_id/captures", _authorization,
                        captureRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => captureResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response =
                await paymentsClient.CapturePayment("payment_id", captureRequest, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRefundPayment_Id()
        {
            var refundResponse = new RefundResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RefundResponse>(PaymentsPath + "/payment_id/refunds", _authorization,
                        null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => refundResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RefundPayment("payment_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRefundPayment_IdempotencyKey()
        {
            var refundResponse = new RefundResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RefundResponse>(PaymentsPath + "/payment_id/refunds", _authorization,
                        null,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => refundResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RefundPayment("payment_id", null, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRefundPayment_Request()
        {
            var refundRequest = new RefundRequest();
            var refundResponse = new RefundResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RefundResponse>(PaymentsPath + "/payment_id/refunds", _authorization,
                        refundRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => refundResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response =
                await paymentsClient.RefundPayment("payment_id", refundRequest, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment_Id()
        {
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>(PaymentsPath + "/payment_id/voids", _authorization,
                        null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => voidResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.VoidPayment("payment_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment_IdempotencyKey()
        {
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>(PaymentsPath + "/payment_id/voids", _authorization,
                        null,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => voidResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.VoidPayment("payment_id", null, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment_Request()
        {
            var voidRequest = new VoidRequest();
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>(PaymentsPath + "/payment_id/voids", _authorization,
                        voidRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => voidResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.VoidPayment("payment_id", voidRequest, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldIncrementPaymentAuthorization()
        {
            var authorizationRequest = new AuthorizationRequest();
            var authorizationResponse = new AuthorizationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AuthorizationResponse>(PaymentsPath + "/payment_id/authorizations", _authorization,
                        authorizationRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => authorizationResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response =
                await paymentsClient.IncrementPaymentAuthorization("payment_id", authorizationRequest, "test");

            response.ShouldNotBeNull();
        }
    }
}