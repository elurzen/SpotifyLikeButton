using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpotifyLikeButton
{
    internal class SpotifyPkceAuth
    {
        private readonly string _clientId;
        private readonly string _redirectUri;
        private readonly string _scope;
        private string _codeVerifier;
        private readonly string _codeChallenge;

        public SpotifyPkceAuth(string clientId, string redirectUri, string scope)
        {
            LogManager.WriteLog($"Init SpotifyPkceAuth ({clientId}, {redirectUri}, {scope})");

            _clientId = clientId;
            _redirectUri = redirectUri;
            _scope = scope;
            _codeVerifier = GenerateCodeVerifier(64);
            _codeChallenge = GenerateCodeChallenge(_codeVerifier);
        }

        public void SetCodeVerifier(string codeVerifier)
        {
            _codeVerifier = codeVerifier;
        }

        /// <summary>
        /// Generates a random string for the code verifier
        /// </summary>
        private string GenerateCodeVerifier(int length)
        {
            LogManager.WriteLog("Enter GenerateCodeVerifier");
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
            var random = new Random();
            var result = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            LogManager.WriteLog("Exit GenerateCodeVerifier");
            return result.ToString();
        }

        /// <summary>
        /// Creates a code challenge using SHA256 hash of the code verifier
        /// </summary>
        private string GenerateCodeChallenge(string codeVerifier)
        {
            LogManager.WriteLog("Enter GenerateCodeChallenge");
            using var sha256 = SHA256.Create();
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));

            LogManager.WriteLog("Exit GenerateCodeChallenge");
            // Convert to Base64URL encoding (Base64 with URL-safe characters)
            return Convert.ToBase64String(challengeBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        /// <summary>
        /// Gets the authorization URL to redirect the user to
        /// </summary>
        public string GetAuthorizationUrl()
        {
            LogManager.WriteLog("Enter GetAuthorizationUrl");
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["response_type"] = "code";
            queryParams["client_id"] = _clientId;
            queryParams["scope"] = _scope;
            queryParams["code_challenge_method"] = "S256";
            queryParams["code_challenge"] = _codeChallenge;
            queryParams["redirect_uri"] = _redirectUri;

            var uriBuilder = new UriBuilder("https://accounts.spotify.com/authorize")
            {
                Query = queryParams.ToString()
            };

            LogManager.WriteLog("Exit GetAuthorizationUrl");
            return uriBuilder.ToString();
        }

        /// <summary>
        /// Exchanges the authorization code for an access token
        /// </summary>
        public async Task<SpotifyTokenResponse> RequestAccessTokenAsync(string code)
        {
            LogManager.WriteLog("Enter RequestAccessTokenAsync");
            using var httpClient = new HttpClient();

            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", _redirectUri),
                new KeyValuePair<string, string>("code_verifier", _codeVerifier)
            });

            var response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                LogManager.WriteLog($"Error requesting access token: {content}");
                throw new Exception($"Error requesting access token: {content}");
            }

            LogManager.WriteLog("Exit RequestAccessTokenAsync");
            // You might want to use a proper JSON deserializer like Newtonsoft.Json or System.Text.Json here
            // This is a simplified version that assumes the response parsing succeeds
            return System.Text.Json.JsonSerializer.Deserialize<SpotifyTokenResponse>(content);
        }

        /// <summary>
        /// Gets the code verifier (to be stored until the token request)
        /// </summary>
        public string GetCodeVerifier()
        {
            return _codeVerifier;
        }
    }

    public class SpotifyTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}

