using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prj_final_METEO.Models
{
    public class ApiClient : IDisposable
    {
        private string _urlBaseApi;
        private HttpClient _httpClient;

        public ApiClient(string urlBaseApi)
        {
            if (urlBaseApi.EndsWith('/'))
                urlBaseApi = urlBaseApi.Substring(0, urlBaseApi.Length - 1);
            _urlBaseApi = urlBaseApi;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> RequeteGetAsync(string endpoint)
        {
            endpoint += "&key=" + Properties.Settings.Default.JetonKey;

            Trace.WriteLine("Call to : " + _urlBaseApi + endpoint);

            HttpResponseMessage hrm = await _httpClient.GetAsync(_urlBaseApi + endpoint);
            return await hrm.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
