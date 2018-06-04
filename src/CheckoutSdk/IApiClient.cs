using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<ApiResponse<TResult>> PostAsync<TResult>(string path, IApiCredentials credentials, object request = null);
        Task<ApiResponse<dynamic>> PostAsync(string path, IApiCredentials credentials, object request, Dictionary<HttpStatusCode, Type> resultTypeMappings);
    }
}