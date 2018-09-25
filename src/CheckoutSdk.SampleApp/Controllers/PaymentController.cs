using Checkout;
using Checkout.Common;
using Checkout.Payments;
using CheckoutSdk.SampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;

namespace CheckoutSdk.SampleApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICheckoutApi _checkoutApi;
        private readonly IControllerUrlBuilder _controllerUrlBuilder;

        public PaymentController(ICheckoutApi checkoutApi, IControllerUrlBuilder controllerUrlBuilder)
        {
            _checkoutApi = checkoutApi;
            _controllerUrlBuilder = controllerUrlBuilder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PrepareIndexViewData();
            return View();
        }

        private void PrepareIndexViewData()
        {
            ViewData[PaymentModel.CurrenciesViewData] = new[]
            {
                new SelectListItem() {Value = Currency.USD, Text = Currency.USD},
                new SelectListItem() {Value = Currency.EUR, Text = Currency.EUR},
                new SelectListItem() {Value = Currency.GBP, Text = Currency.GBP}
            };
            ViewData[PaymentModel.PublicKeyViewData] = _checkoutApi.PublicKey;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(PaymentModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    PrepareIndexViewData();
                    return View(nameof(Index), model);
                }

                if (string.IsNullOrWhiteSpace(model.CardToken))
                    throw new ArgumentException("Model", $"{nameof(model.CardToken)} is missing.");

                var source = new TokenSource(model.CardToken);
                var paymentRequest = new PaymentRequest<TokenSource>(source, model.Currency, model.Amount)
                {
                    ThreeDs = model.DoThreeDs,
                    SuccessUrl = BuildUrl(nameof(ThreeDsSuccess)),
                    FailureUrl = BuildUrl(nameof(ThreeDsFailure))
                };

                var response = await _checkoutApi.Payments.RequestAsync(paymentRequest);

                if (response.IsPending)
                {
                    return Redirect(response.Pending.GetRedirectLink().Href);
                }

                if (response.Payment.Approved)
                {
                    return View("NonThreeDsSuccess", response.Payment);
                }

                return View("NonThreeDsFailure", response.Payment);
            }
            catch (Exception ex)
            {
                return View(nameof(Error), new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier, Message = ex.Message });
            }
        }

        private string BuildUrl(string actionName)
        {
            return _controllerUrlBuilder.Build(this, actionName);
        }

        public async Task<IActionResult> ThreeDsSuccess([FromQuery(Name = "cko-session-id")] string ckoSessionId)
        {
            GetPaymentDetailsResponse details = await _checkoutApi.Payments.GetAsync(ckoSessionId);

            return View(details);
        }

        public async Task<IActionResult> ThreeDsFailure([FromQuery(Name = "cko-session-id")] string ckoSessionId)
        {
            GetPaymentDetailsResponse details = await _checkoutApi.Payments.GetAsync(ckoSessionId);

            return View(details);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
