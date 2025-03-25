using System.Configuration;
using System.Diagnostics;
using System.Xml.Linq;
namespace SpotifyLikeButton
{
    internal static class Program
    {
        public const string LOGPATH = "./SpotifyLikeButton.log";
        public static SpotifyHotkeyManager hotKeyManager;

        [STAThread]
        static void Main()
        {
            if (SpotifyLikeButtonSettings.Default.EnableLogging)
            {
                LogManager.ConfigureTraceListener(true);
                LogManager.WriteLog("Enter Main");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            checkFirstRun();
            initTrayIcon();
            AuthorizeWithSpotify();
            Application.Run();
            
        }

        private static void checkFirstRun()
        {
            LogManager.WriteLog("Enter checkFirstRun");
            if (SpotifyLikeButtonSettings.Default.FirstRunCompleted)
            {
                LogManager.WriteLog("Exit checkFirstRun - Not first run");
                return;
            }
            SpotifyLikeButtonSettings.Default.LikeSongHotkey = "F4";
            SpotifyLikeButtonSettings.Default.UnlikeSongHotkey = "F8";
            SpotifyLikeButtonSettings.Default.LikeSongSound = "./NotificationSounds\\Bird Whistle.wav";
            SpotifyLikeButtonSettings.Default.UnlikeSongSound = "./NotificationSounds\\Lump Dump.wav";
            SpotifyLikeButtonSettings.Default.ErrorSound = "./NotificationSounds\\Nuh Uh.wav";
            SpotifyLikeButtonSettings.Default.LaunchOnStartup = false;
            SpotifyLikeButtonSettings.Default.ShowNotifications = false;
            SpotifyLikeButtonSettings.Default.EnableLogging = false;
            SpotifyLikeButtonSettings.Default.FirstRunCompleted = true;            
            SpotifyLikeButtonSettings.Default.Save();

            LogManager.WriteLog("Exit checkFirstRun - Defaults set");
        }
        private static void initTrayIcon()
        {            
            NotifyIcon trayIcon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(".\\icon.ico"),
                Visible = true,
                Text = "Spotify Like Button"
            };

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Settings", null, (s, e) =>
            {
                var settingsForm = new SettingsForm();
                settingsForm.ShowDialog();
            });

            menu.Items.Add("Exit", null, (s, e) =>
            {
                hotKeyManager?.Dispose(); // Unhook before exiting
                trayIcon.Dispose();
                Application.Exit();
            });

            trayIcon.ContextMenuStrip = menu;

            trayIcon.MouseDoubleClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    var settingsForm = new SettingsForm();
                    settingsForm.ShowDialog();
                }
            };
        }

        private async static void AuthorizeWithSpotify()
        {
            try
            {
                LogManager.WriteLog("Enter AuthorizeWithSpotify");
                string CLIENT_ID = "39148cb6a55e4f89a8b89ff1bb9f0ba1";
                var accessToken = SpotifyLikeButtonSettings.Default.AccessToken;
                var expirationToken = SpotifyLikeButtonSettings.Default.TokenExpirationTime;
                var refreshToken = SpotifyLikeButtonSettings.Default.RefreshToken;

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    LogManager.WriteLog("Exit AuthorizeWithSpotify - Using refresh token to authorize with Spotify");
                    hotKeyManager = new SpotifyHotkeyManager(CLIENT_ID);
                    return;
                }

                string REDIRECT_URI = "http://localhost:8080/callback";
                string SCOPE = "user-read-currently-playing user-library-read user-library-modify";

                var redirectHandler = new SpotifyAuthRedirectHandler(CLIENT_ID, REDIRECT_URI, SCOPE);
                string authorizationCode = await redirectHandler.GetAuthorizationCodeAsync();
                var tokenResponse = await redirectHandler.ExchangeCodeForTokenAsync(authorizationCode);

                DateTime TokenExpirationTime = DateTime.UtcNow.AddSeconds(Convert.ToDouble(tokenResponse.expires_in));
                SpotifyLikeButtonSettings.Default.AccessToken = tokenResponse.access_token;
                SpotifyLikeButtonSettings.Default.TokenExpirationTime = TokenExpirationTime;
                SpotifyLikeButtonSettings.Default.RefreshToken = tokenResponse.refresh_token;                
                SpotifyLikeButtonSettings.Default.Save();

                LogManager.WriteLog("Succesfully Authorized with Spotify");
                LogManager.WriteLog($"Access Token: {tokenResponse.access_token}");
                LogManager.WriteLog($"Token Expiration Time: {TokenExpirationTime}");
                LogManager.WriteLog($"Refresh Token: {tokenResponse.refresh_token}");

                hotKeyManager = new SpotifyHotkeyManager(CLIENT_ID);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog($"Authentication error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    LogManager.WriteLog($"Inner exception: {ex.InnerException.Message}");
                }                
            }

            LogManager.WriteLog("Exit AuthorizeWithSpotify");
        }
    }
}