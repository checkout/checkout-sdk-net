using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Response;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Product = Checkout.Payments.Request.Product;

namespace Checkout.Payments
{
    public class CapturePaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldFullCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest
            {
                Amount = 10,
                CaptureType = CaptureType.Final,
                Reference = Guid.NewGuid().ToString(),
                Customer =
                    new PaymentCustomerRequest
                    {
                        Email = GenerateRandomEmail(),
                        Name = "Bruce Wayne",
                        Phone = GetPhone(),
                        TaxNumber = "1350693505279"
                    },
                Description = "Set of 3 masks",
                BillingDescriptor =
                    new BillingDescriptor
                    {
                        Name = "SUPERHEROES.COM", City = "GOTHAM", Reference = Guid.NewGuid().ToString()
                    },
                Shipping =
                    new ShippingDetails {Address = GetAddress(), Phone = GetPhone(), FromAddressZip = "10014"},
                Items =
                    new List<Product>
                    {
                        new Product
                        {
                            Name = "Kevlar batterang",
                            Quantity = 2,
                            UnitPrice = 50,
                            Reference = Guid.NewGuid().ToString(),
                            CommodityCode = "DEF123",
                            UnitOfMeasure = "metres",
                            TotalAmount = 19000,
                            TaxAmount = 1000,
                            DiscountAmount = 1000,
                            WxpayGoodsId = "1001"
                        }
                    },
                Processing = new ProcessingSettings
                {
                    OrderId = "123456789",
                    TaxAmount = 3000,
                    DiscountAmount = 0,
                    DutyAmount = 0,
                    ShippingAmount = 300,
                    ShippingTaxAmount = 100,
                    Aft = true,
                    PreferredScheme = PreferredSchema.Mastercard,
                    MerchantInitiatedReason = MerchantInitiatedReason.DelayedCharge,
                    ProductType = ProductType.QrCode,
                    OpenId = "oUpF8uMuAJO_M2pxb1Q9zNjWeS6o",
                    OriginalOrderAmount = 10,
                    ReceiptId = "10",
                    TerminalType = TerminalType.Wap,
                    OsType = OsType.Android,
                    InvoiceId = Guid.NewGuid().ToString(),
                    BrandName = "Super Brand",
                    Locale = "en-US",
                    ShippingPreference = ShippingPreference.SetProvidedAddress,
                    UserAction = UserAction.PayNow,
                    AirlineData = new List<AirlineData>
                    {
                        new AirlineData
                        {
                            Ticket =
                                new Ticket
                                {
                                    Number = "123456",
                                    IssueDate = "SATE",
                                    IssuingCarrierCode = "ST",
                                    TravelAgencyName = "AGENCY",
                                    TravelAgencyCode = "CODE"
                                },
                            Passenger =
                                new Passenger
                                {
                                    Name = new PassengerName {FullName = "passenger"},
                                    DateOfBirth = "01-01-01",
                                    CountryCode = CountryCode.AC
                                },
                            FlightLegDetails = new List<FlightLegDetails>
                            {
                                new FlightLegDetails
                                {
                                    FlightNumber = 123,
                                    CarrierCode = "code",
                                    ServiceClass = "class",
                                    DepartureDate = "DepartureDate",
                                    DepartureTime = "time",
                                    DepartureAirport = "airport",
                                    ArrivalAirport = "arrival",
                                    StopoverCode = "StopoverCode",
                                    FareBasisCode = "basis"
                                }
                            }
                        }
                    },
                    LineOfBusiness = "Flights"
                }
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await DefaultApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id), TotalCapturedIs10);

            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        private static bool TotalCapturedIs10(GetPaymentResponse obj)
        {
            return obj.Balances.TotalCaptured == 10;
        }

        [Fact]
        private async Task ShouldPartiallyCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString(), Amount = paymentResponse.Amount / 2
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await DefaultApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id), TotalCapturedIs5);

            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        private static bool TotalCapturedIs5(GetPaymentResponse obj)
        {
            return obj.Balances.TotalCaptured == 5;
        }

        [Fact]
        private async Task ShouldCaptureCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString()};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var capture1 = await DefaultApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture1.ShouldNotBeNull();

            var capture2 = await DefaultApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture2.ShouldNotBeNull();

            capture1.ActionId.ShouldBe(capture2.ActionId);
        }
    }
}