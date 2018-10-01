using Checkout.Common;
using Checkout.Payments;
using Checkout.SampleApp.Models;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Checkout.SampleApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ICheckoutApi _checkoutApi;
        private readonly CheckoutConfiguration _configuration;
        private readonly ISerializer _serializer;

        public PaymentsController(ICheckoutApi checkoutApi, CheckoutConfiguration configuration, ISerializer serializer)
        {
            _checkoutApi = checkoutApi ?? throw new ArgumentNullException(nameof(checkoutApi));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        [HttpGet]
        public IActionResult Index()
        {
            PaymentModel model = PrepareModel(null);
            return View(model);
        }

        private PaymentModel PrepareModel(PaymentModel existingModel)
        {
            var model = existingModel ?? new PaymentModel();
            model.Currencies = new[]
            {
                new SelectListItem() {Value = Currency.USD, Text = Currency.USD},
                new SelectListItem() {Value = Currency.EUR, Text = Currency.EUR},
                new SelectListItem() {Value = Currency.GBP, Text = Currency.GBP}
            };
            model.PublicKey = _configuration.PublicKey;
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(PaymentModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    PrepareModel(model);
                    return View(nameof(Index), model);
                }

                if (string.IsNullOrWhiteSpace(model.CardToken))
                    throw new ArgumentException("Model", $"{nameof(model.CardToken)} is missing.");

                var source = new TokenSource(model.CardToken);
                var paymentRequest = new PaymentRequest<TokenSource>(source, model.Currency, model.Amount)
                {
                    ThreeDS = model.DoThreeDS,
                    SuccessUrl = BuildUrl(nameof(ThreeDSSuccess)),
                    FailureUrl = BuildUrl(nameof(ThreeDSFailure))
                };

                var response = await _checkoutApi.Payments.RequestAsync(paymentRequest);

                if (response.IsPending && response.Pending.RequiresRedirect())
                {
                    return Redirect(response.Pending.GetRedirectLink().Href);
                }

                TempData[response.Payment.Id] = _serializer.Serialize(response.Payment);

                if (response.Payment.Approved)
                {
                    return RedirectToAction(nameof(NonThreeDSSuccess), new { paymentId = response.Payment.Id });
                }

                return RedirectToAction(nameof(NonThreeDSFailure), new { paymentId = response.Payment.Id });
            }
            catch (Exception ex)
            {
                return View(nameof(Error), new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier, Message = ex.Message });
            }
        }

        private string BuildUrl(string actionName)
        {
            var uriBuilder = new UriBuilder(Request.GetUri())
            {
                Path = Url.Action(actionName, ControllerContext.RouteData.Values["controller"].ToString())
            };
            return uriBuilder.Uri.ToString();
        }

        public async Task<IActionResult> ThreeDSSuccess([FromQuery(Name = "cko-session-id")] string ckoSessionId)
        {
            GetPaymentDetailsResponse details = await _checkoutApi.Payments.GetAsync(ckoSessionId);
            return View(details);
        }

        public async Task<IActionResult> ThreeDSFailure([FromQuery(Name = "cko-session-id")] string ckoSessionId)
        {
            GetPaymentDetailsResponse details = await _checkoutApi.Payments.GetAsync(ckoSessionId);
            return View(details);
        }

        public IActionResult NonThreeDSSuccess(string paymentId)
        {
            var serialized = (string)TempData[paymentId];
            var payment = (PaymentProcessed)_serializer.Deserialize(serialized, typeof(PaymentProcessed));
            return View(payment);
        }

        public IActionResult NonThreeDSFailure(string paymentId)
        {
            var serialized = (string)TempData[paymentId];
            var payment = (PaymentProcessed)_serializer.Deserialize(serialized, typeof(PaymentProcessed));
            return View(payment);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
