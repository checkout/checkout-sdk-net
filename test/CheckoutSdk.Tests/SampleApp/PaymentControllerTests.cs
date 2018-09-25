using Checkout.Common;
using Checkout.Payments;
using CheckoutSdk.SampleApp.Controllers;
using CheckoutSdk.SampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Tests.SampleApp
{
    public class PaymentControllerTests
    {
        private readonly Mock<ICheckoutApi> _checkoutApi;
        private readonly Mock<IPaymentsClient> _paymentsClient;
        private readonly PaymentController _controller;
        private readonly PaymentResponse _paymentsResponse;

        public PaymentControllerTests()
        {
            _checkoutApi = new Mock<ICheckoutApi>();
            _checkoutApi.SetupGet(a => a.PublicKey).Returns("");

            _paymentsResponse = new PaymentResponse()
            {
                Payment = new PaymentProcessed()
            };
            _paymentsClient = new Mock<IPaymentsClient>();
            _paymentsClient.Setup(p =>
                    p.RequestAsync(It.IsAny<PaymentRequest<TokenSource>>(), default(CancellationToken)))
                .ReturnsAsync(() => _paymentsResponse);
            _paymentsClient.Setup(p =>
                p.GetAsync(It.IsAny<string>(), default(CancellationToken)))
                    .ReturnsAsync(() => new GetPaymentDetailsResponse());

            var urlBuilder = new Mock<IControllerUrlBuilder>();
            urlBuilder.Setup(b => b.Build(It.IsAny<ControllerBase>(), It.IsAny<string>()))
                .Returns("test");
            
            _controller = new PaymentController(_checkoutApi.Object, urlBuilder.Object);
        }

        [Fact]
        public void CanRenderIndexView()
        {
            var result = _controller.Index();

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewData.ModelState.IsValid.ShouldBeTrue();
            viewResult.ViewName.ShouldBeNull();
        }

        [Fact]
        public async Task GivenModelInvalidRenderIndexViewWithModelErrors()
        {
            _controller.ModelState.AddModelError("", "test");
            var model = new PaymentModel();

            var result = await _controller.Post(model);

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewData.ModelState.IsValid.ShouldBeFalse();
            viewResult.ViewName.ShouldBe(nameof(PaymentController.Index));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public async Task GivenCardTokenInvalidRenderErrorView(string cardToken)
        {
            var model = CreateValidModel();
            model.CardToken = cardToken;

            var result = await _controller.Post(model);

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewName.ShouldBe(nameof(PaymentController.Error));
            viewResult.Model.ShouldNotBeNull();
            var errorModel = viewResult.Model.ShouldBeAssignableTo<ErrorViewModel>();
            errorModel.Message.ShouldNotBeNullOrWhiteSpace();
        }

        private PaymentModel CreateValidModel()
        {
            return new PaymentModel()
            {
                Amount = 3,
                Currency = "USD",
                DoThreeDs = false,
                CardToken = "test"
            };
        }

        [Fact]
        public async Task GivenExceptionThrownRenderErrorView()
        {
            _checkoutApi.SetupGet(a => a.Payments).Returns(() => throw new Exception());

            var result = await _controller.Post(new PaymentModel());

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewName.ShouldBe(nameof(PaymentController.Error));
            viewResult.Model.ShouldNotBeNull();
            var errorModel = viewResult.Model.ShouldBeAssignableTo<ErrorViewModel>();
            errorModel.Message.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task CanRenderNonThreeDsSuccessView()
        {
            _checkoutApi.Setup(a => a.Payments).Returns(_paymentsClient.Object);
            _paymentsResponse.Payment.Approved = true;
            var model = CreateValidModel();
            model.DoThreeDs = false;

            var result = await _controller.Post(model);

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewName.ShouldBe("NonThreeDsSuccess");
            var modelResult = viewResult.Model.ShouldBeAssignableTo<PaymentProcessed>();
            modelResult.Approved.ShouldBeTrue();
        }

        [Fact]
        public async Task CanRenderNonThreeDsFailureView()
        {
            _checkoutApi.Setup(a => a.Payments).Returns(_paymentsClient.Object);
            _paymentsResponse.Payment.Approved = false;
            var model = CreateValidModel();
            model.DoThreeDs = false;

            var result = await _controller.Post(model);

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewName.ShouldBe("NonThreeDsFailure");
            var modelResult = viewResult.Model.ShouldBeAssignableTo<PaymentProcessed>();
            modelResult.Approved.ShouldBeFalse();
        }

        [Fact]
        public async Task CanRedirectThreeDs()
        {
            _checkoutApi.Setup(a => a.Payments).Returns(_paymentsClient.Object);
            _paymentsResponse.Pending = new PaymentPending();
            var redirectLink = new Link() { Href = "test" };
            _paymentsResponse.Pending.Links.Add("redirect", redirectLink);
            var model = CreateValidModel();
            model.DoThreeDs = true;

            var result = await _controller.Post(model);

            var viewResult = result.ShouldBeAssignableTo<RedirectResult>();
            viewResult.Url.ShouldBe(redirectLink.Href);
        }

        [Fact]
        public async Task CanRenderThreeDsSuccessView()
        {
            _checkoutApi.Setup(a => a.Payments).Returns(_paymentsClient.Object);
            var result = await _controller.ThreeDsSuccess("test");

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewName.ShouldBeNull();
            viewResult.Model.ShouldBeAssignableTo<GetPaymentDetailsResponse>();
        }

        [Fact]
        public async Task CanRenderThreeDsFailureView()
        {
            _checkoutApi.Setup(a => a.Payments).Returns(_paymentsClient.Object);
            var result = await _controller.ThreeDsFailure("test");

            var viewResult = result.ShouldBeAssignableTo<ViewResult>();
            viewResult.ViewName.ShouldBeNull();
            viewResult.Model.ShouldBeAssignableTo<GetPaymentDetailsResponse>();
        }
    }
}
