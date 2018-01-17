using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
namespace Michal.Balador.SimpleMessage
{
    public class HttpClientTest
    {
        private HttpClient _httpClient;
        public HttpClientTest()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://runnerdevice.co.il/api/ping")
            };
            _httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public void  Disconnect()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
        }
            public async Task<T> GetAsync<T>(string  args )
        {
            var response = await _httpClient.GetAsync($"?s={args}");


            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
              //  return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async  Task<string> SendMessage(string v)
        {
            var decodeurl= HttpUtility.UrlDecode(v);
            return await GetAsync<string>(decodeurl);
        }
    }
}
