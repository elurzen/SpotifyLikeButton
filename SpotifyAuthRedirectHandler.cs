using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SpotifyLikeButton;

public class SpotifyAuthRedirectHandler
{
    private readonly string _clientId;
    private readonly string _redirectUri;    
    private readonly string _scope;
    private string _codeVerifier;
    private HttpListener _httpListener;
    private TaskCompletionSource<string> _authCodeTaskSource;

    public SpotifyAuthRedirectHandler(string clientId, string redirectUri, string scope)
    {
        _clientId = clientId;
        _redirectUri = redirectUri;
        _scope = scope;
        

        // Validate that the redirect URI is a valid local URI for HttpListener
        if (!_redirectUri.StartsWith("http://localhost") && !_redirectUri.StartsWith("http://127.0.0.1"))
        {
            throw new ArgumentException("Redirect URI must be a local URI starting with http://localhost or http://127.0.0.1");
        }
    }

    public async Task<string> GetAuthorizationCodeAsync()
    {
        _authCodeTaskSource = new TaskCompletionSource<string>();

        // Start the HTTP listener
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add(_redirectUri.EndsWith("/") ? _redirectUri : _redirectUri + "/");
        _httpListener.Start();

        // Start listening for the redirect asynchronously
        _httpListener.GetContextAsync().ContinueWith(HandleRedirectResponse);

        // Open the browser with the auth URL
        var auth = new SpotifyPkceAuth(_clientId, _redirectUri, _scope);

        // Store the code verifier for later use (using one of the secure storage methods)
        _codeVerifier = auth.GetCodeVerifier();

        // Open the browser with the authorization URL
        string authUrl = auth.GetAuthorizationUrl();
        OpenBrowser(authUrl);

        // Wait for the redirect to be received
        //Debug.WriteLine("Waiting for authorization response...");
        string authorizationCode = await _authCodeTaskSource.Task;
        //Debug.WriteLine($"Authorization code received: {authorizationCode}");

        return authorizationCode;
    }

    public async Task<SpotifyTokenResponse> ExchangeCodeForTokenAsync(string authorizationCode)
    {
        // Create a new instance of SpotifyPkceAuth with the same parameters
        var auth = new SpotifyPkceAuth(_clientId, _redirectUri, _scope);
        auth.SetCodeVerifier(_codeVerifier);

        // Exchange the code for a token
        return await auth.RequestAccessTokenAsync(authorizationCode);
    }

    private void HandleRedirectResponse(Task<HttpListenerContext> contextTask)
    {
        try
        {
            var context = contextTask.Result;
            var request = context.Request;
            var response = context.Response;

            // Parse the authorization code from the query string
            string code = request.QueryString["code"];
            string error = request.QueryString["error"];

            if (!string.IsNullOrEmpty(error))
            {
                _authCodeTaskSource.SetException(new Exception($"Authorization error: {error}"));
            }
            else if (string.IsNullOrEmpty(code))
            {
                _authCodeTaskSource.SetException(new Exception("No authorization code received"));
            }
            else
            {
                _authCodeTaskSource.SetResult(code);
            }

            // Return a success page to the user
            string responseString = "<html><body style='background:#222;color:white'><h1>Authorization Successful</h1><p>You can now close this window and return to the application.</p></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/html";
            var responseOutput = response.OutputStream;
            responseOutput.Write(buffer, 0, buffer.Length);
            responseOutput.Close();

            // Stop the listener
            _httpListener.Stop();
        }
        catch (Exception ex)
        {
            //Debug.WriteLine($"Error handling redirect: {ex.Message}");
            _authCodeTaskSource.SetException(ex);
        }
    }

    // Helper method to open the default browser
    private void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // Handle platform-specific browser launching
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}
