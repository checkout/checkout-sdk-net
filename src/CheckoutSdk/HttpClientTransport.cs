﻿using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public class HttpClientTransport : ITransport
    {
        private readonly HttpClient _httpClient;
        private readonly CheckoutConfiguration _configuration;
        private readonly ILogger _log = LogProvider.GetLogger(typeof(HttpClientTransport));

        public HttpClientTransport(CheckoutConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = GetHttpClient(configuration);
        }

        private static HttpClient GetHttpClient(CheckoutConfiguration configuration)
        {
            var httpClient = configuration.HttpClientFactory.CreateClient();
            CheckoutUtils.ValidateParams("httpClient", httpClient);
            return httpClient;
        }

        public async Task<HttpResponseMessage> Invoke(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            HttpContent httpContent,
            CancellationToken cancellationToken,
            string idempotencyKey,
            bool useFileUri = false)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "path", path, "authorization", authorization);
            var httpRequest = new HttpRequestMessage(httpMethod, GetRequestUri(path, useFileUri))
            {
                Content = httpContent
            };
            _log.LogInformation(@"{HttpMethod}: {Path}", httpMethod, path);

            httpRequest.Headers.UserAgent.ParseAdd(
                "checkout-sdk-net/" + CheckoutUtils.GetAssemblyVersion<CheckoutSdk>());
            httpRequest.Headers.Authorization = authorization.GetAuthorizationHeader();

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
            {
                httpRequest.Headers.Add("Cko-Idempotency-Key", idempotencyKey);
            }

            return await _httpClient.SendAsync(httpRequest, cancellationToken);
        }

        private Uri GetRequestUri(string path, bool useFileUri = false)
        {
            Uri uri;
            if (useFileUri)
            {
                uri = _configuration.Environment.GetAttribute<EnvironmentAttribute>().FileUri;                
            }
            else
            {
                uri = _configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri;
            }

            Uri.TryCreate(uri, path, out uri);

            return uri;
        }
    }
}