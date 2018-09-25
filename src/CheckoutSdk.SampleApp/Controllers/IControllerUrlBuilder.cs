using System;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutSdk.SampleApp.Controllers
{
    public interface IControllerUrlBuilder
    {
        string Build(ControllerBase controller, string actionName);
    }

    public class ControllerControllerUrlBuilder : IControllerUrlBuilder
    {
        public string Build(ControllerBase controller, string actionName)
        {
            var uriBuilder = new UriBuilder(controller.Request.GetUri())
            {
                Path = controller.Url.Action(actionName, controller.ControllerContext.ActionDescriptor.ControllerName)
            };
            return uriBuilder.Uri.ToString();
        }
    }
}