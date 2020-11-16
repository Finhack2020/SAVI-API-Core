using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Infrastructure
{
    public class HttpSenderApi
    {

        // Refatorar - remover polimorfismo e simplificando em um metodo apenas...
        public static async Task<string> Call()
        {
            var urlbase = Environment.GetEnvironmentVariable("URLAUTHToken");
            string clientSecret = Environment.GetEnvironmentVariable("CLIENTSECRET");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", clientSecret);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = Environment.GetEnvironmentVariable("CONFIG_REQUEST_BODY");
            var data = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(urlbase, data);
            var result = await response.Content.ReadAsStringAsync();

            Auth AuthDeserialized = JsonConvert.DeserializeObject<Auth>(result);

            return AuthDeserialized.Access_token;

        }

        public static async Task<string> Call(string url, string token,string json)
        {
            var urlbase = Environment.GetEnvironmentVariable("URLBASE");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new StringContent(json);

            var response = await client.PostAsync(urlbase + url, data);
            var result = await response.Content.ReadAsStringAsync();

            Auth AuthDeserialized = JsonConvert.DeserializeObject<Auth>(result);

            return AuthDeserialized.Access_token;

        }

        public static async Task<string> Call(string url, string token)
        {
            var urlbase = Environment.GetEnvironmentVariable("URLBASE");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync(urlbase + url);

            var content = await result.Content.ReadAsStringAsync();

            return content;
        }

    }
    public class Auth
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
    }
}
