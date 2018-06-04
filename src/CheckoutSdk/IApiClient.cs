using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<ApiResponse<TResult>> PostAsync<TResult>(string path, IApiCredentials credentials, CancellationToken cancellationToken, object request = null);
        Task<ApiResponse<dynamic>> PostAsync(string path, IApiCredentials credentials, object request, Dictionary<HttpStatusCode, Type> resultTypeMappings, CancellationToken cancellationToken);
    }
}