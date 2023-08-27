using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace Server.DL
{
    public class DiscordLogic
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public DiscordLogic(IConfiguration config, HttpClient client)
        {
            _config = config;
            _client = client;
        }
        public async Task<object> GetToken(string code, string redirectUrl)
        {

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://discord.com/api/oauth2/token");

            var data = new Dictionary<string, string>()
            {
                {"client_id", _config["Discord:Client_ID"]},
                {"client_secret", _config["Discord:Client_Secret"]},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", redirectUrl},
                {"scope", "identify"}
            };

            req.Content = new FormUrlEncodedContent(data);

            await Console.Out.WriteLineAsync(req.Content.ReadAsStringAsync().Result);

            var result = await _client.SendAsync(req);

            var status = result.EnsureSuccessStatusCode();
            if (status.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();

                var json = JsonNode.Parse(response);

                return json;
            }
            return "";
        }

        public async Task<string> GetUser(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var me = await _client.GetAsync("https://discord.com/api/users/@me");

            return me.Content.ReadAsStringAsync().Result;
        }
    }
}
