using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<ApiResponse<TResult>> PostAsync<TResult>(string path, object request = null);
        Task<ApiResponse<dynamic>> PostAsync(string path, object request, Dictionary<HttpStatusCode, Type> resultTypeMappings);
    }
}