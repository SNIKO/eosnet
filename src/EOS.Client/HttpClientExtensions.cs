using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EOS.Client
{
    public static class HttpClientExtensions
    {
        public static Task<T> GetAsync<T>(this HttpClient client, string uri)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri);
            return client.SendAsync<T>(msg);
        }

        public static Task GetAsync(this HttpClient client, string uri)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri);
            return client.SendAsync(msg);
        }

        public static Task<T> PostAsync<T>(this HttpClient client, string uri, object content)
        {
            var json = JsonConvert.SerializeObject(content);
            return client.PostAsync<T>(uri, json);
        }

        public static Task<T> PostAsync<T>(this HttpClient client, string uri, string content)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(content)
            };

            return client.SendAsync<T>(msg);
        }

        public static Task PostAsync(this HttpClient client, string uri, string content)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(content)
            };

            return client.SendAsync(msg);
        }

        private static async Task SendAsync(this HttpClient client, HttpRequestMessage msg)
        {
            var response = await client.SendAsync(msg).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var errMsg = string.IsNullOrEmpty(content)
                    ? response.ReasonPhrase
                    : content;

                throw new HttpResponseException((int) response.StatusCode, errMsg);
            }
        }

        private static async Task<T> SendAsync<T>(this HttpClient client, HttpRequestMessage msg)
        {
            var response = await client.SendAsync(msg).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var errMsg = string.IsNullOrEmpty(content)
                    ? response.ReasonPhrase
                    : content;

                throw new HttpResponseException((int) response.StatusCode, errMsg);
            }

            var info = JsonConvert.DeserializeObject<T>(content);
            return info;
        }
    }
}