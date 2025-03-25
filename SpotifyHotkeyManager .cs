using SpotifyLikeButton;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

public class SpotifyHotkeyManager : IDisposable
{
    // Import the necessary Windows API functions for global keyboard hooks
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr hookId);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hookId, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [StructLayout(LayoutKind.Sequential)]
    private struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    // Define the callback delegate for the hook
    private delegate IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam);

    // Constants for the keyboard hook
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private const int WM_SYSKEYDOWN = 0x0104;
    private const int WM_SYSKEYUP = 0x0105;
    private const int VK_ZERO = 189;  //- or G5 Key
    private const int VK_G4 = 48;  //- or G4 Key
    private const int VK_CONTROL = 162;   // Control key
    private const int VK_SHIFT = 160;     // Shift key
    private const int VK_ALT = 164;   // Alt key
    private const int VERIFICATION_INTERVAL = 5000; // Check every 5 seconds

    private IntPtr _hookId = IntPtr.Zero;
    private KeyboardHookProc _proc;
    private bool _ctrlKeyDown = false;
    private bool _altKeyDown = false;
    private bool _shiftKeyDown = false;
    private string _accessToken;
    private string _clientId;
    private string _refreshToken;
    private DateTime _tokenExpirationTime;

    private bool _likeSongUseCtrl;
    private bool _likeSongUseShift;
    private bool _likeSongUseAlt;
    private int _likeSongKeyCode;

    private bool _unlikeSongUseCtrl;
    private bool _unlikeSongUseShift;
    private bool _unlikeSongUseAlt;
    private int _unlikeSongKeyCode;

    private string _LikeSongSound;
    private string _UnlikeSongSound;
    private string _ErrorSound;

    public SpotifyHotkeyManager(string clientId)
    {
        _clientId = clientId;
        _accessToken = SpotifyLikeButtonSettings.Default.AccessToken;
        _refreshToken = SpotifyLikeButtonSettings.Default.RefreshToken;
        _tokenExpirationTime = SpotifyLikeButtonSettings.Default.TokenExpirationTime;
        LoadSettings();
        _proc = HookCallback;
        _hookId = SetHook(_proc);

        //Debug.WriteLine("Hotkey manager initialized.");
    }

    public void Dispose()
    {
        if (_hookId != IntPtr.Zero)
        {
            UnhookWindowsHookEx(_hookId);
            _hookId = IntPtr.Zero;
        }
    }

    public void ReloadSettings()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        // Parse like song hotkey
        ParseHotkey(
            SpotifyLikeButtonSettings.Default.LikeSongHotkey,
            out _likeSongUseCtrl,
            out _likeSongUseShift,
            out _likeSongUseAlt,
            out _likeSongKeyCode
        );

        // Parse unlike song hotkey
        ParseHotkey(
            SpotifyLikeButtonSettings.Default.UnlikeSongHotkey,
            out _unlikeSongUseCtrl,
            out _unlikeSongUseShift,
            out _unlikeSongUseAlt,
            out _unlikeSongKeyCode
        );

        _LikeSongSound = SpotifyLikeButtonSettings.Default.LikeSongSound;
        _UnlikeSongSound = SpotifyLikeButtonSettings.Default.UnlikeSongSound;
        _ErrorSound = SpotifyLikeButtonSettings.Default.ErrorSound;
    }

    public static bool ParseHotkey(string hotkeyString, out bool ctrl, out bool shift, out bool alt, out int keyCode)
    {
        ctrl = false;
        shift = false;
        alt = false;
        keyCode = 0;

        if (string.IsNullOrWhiteSpace(hotkeyString))
            return false;

        string[] parts = hotkeyString.Split('+');
        if (parts.Length == 0)
            return false;

        // Get main key (last part)
        string keyName = parts[parts.Length - 1].Trim();

        // Check modifiers
        foreach (string part in parts)
        {
            string trimmedPart = part.Trim();
            if (trimmedPart.Equals("Control", StringComparison.OrdinalIgnoreCase))
                ctrl = true;
            else if (trimmedPart.Equals("Shift", StringComparison.OrdinalIgnoreCase))
                shift = true;
            else if (trimmedPart.Equals("Alt", StringComparison.OrdinalIgnoreCase))
                alt = true;
        }

        // Handle key name to virtual key code conversion
        if (Enum.TryParse<Keys>(keyName, true, out Keys key))
        {
            keyCode = (int)key;
            return true;
        }

        return false;
    }

    public void PlaySound (string soundFile)
    {
        if (!string.IsNullOrEmpty(soundFile) && File.Exists(soundFile))
        {
            using (var soundPLayer = new SoundPlayer(soundFile))
            {
                soundPLayer.Play();
            }
        }
    }

    private IntPtr SetHook(KeyboardHookProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode < 0)
            return CallNextHookEx(_hookId, nCode, wParam, lParam); ;

        var hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

        if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr) WM_SYSKEYDOWN)
        {            
            // Keep track of control key state
            if (hookStruct.vkCode == VK_CONTROL)
            {
                _ctrlKeyDown = true;
            }

            if (hookStruct.vkCode == VK_ALT)
            {
                _altKeyDown = true;
            }

            if (hookStruct.vkCode == VK_SHIFT)
            {
                _shiftKeyDown = true;
            }

            // Check if Ctrl+Plus is pressed
            if (hookStruct.vkCode == _likeSongKeyCode &&
                    _ctrlKeyDown == _likeSongUseCtrl &&
                    _shiftKeyDown == _likeSongUseShift &&
                    _altKeyDown == _likeSongUseAlt)
            {
                _ = SaveTrackAsync();
                return CallNextHookEx(_hookId, nCode, wParam, lParam);
            }

            if (hookStruct.vkCode == _unlikeSongKeyCode &&
                    _ctrlKeyDown == _unlikeSongUseCtrl &&
                    _shiftKeyDown == _unlikeSongUseShift &&
                    _altKeyDown == _unlikeSongUseAlt)
            {
                _ = RemoveTrackAsync();
                return CallNextHookEx(_hookId, nCode, wParam, lParam);
            }
        }
        else if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP) // WM_KEYUP
        {
            // Reset control key state
            if (hookStruct.vkCode == VK_CONTROL)
            {
                _ctrlKeyDown = false;
            }

            if (hookStruct.vkCode == VK_ALT)
            {
                _altKeyDown = false;
            }

            if (hookStruct.vkCode == VK_SHIFT)
            {
                _shiftKeyDown = false;
            }
        }

        return CallNextHookEx(_hookId, nCode, wParam, lParam);
    }

    private async Task<CurrentlyPlayingTrack> GetCurrentlyPlayingTrack()
    {
        try
        {
            await RefreshTokenIfNeededAsync();

            using (var httpClient = new HttpClient())
            {
                // Add authorization header
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                // Get currently playing track
                var response = await httpClient.GetAsync("https://api.spotify.com/v1/me/player/currently-playing");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await RefreshAccessTokenAsync();

                    // Retry with new token
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    response = await httpClient.GetAsync("https://api.spotify.com/v1/me/player/currently-playing");
                }

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check if response is not empty
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        var currentlyPlaying = JsonSerializer.Deserialize<CurrentlyPlayingResponse>(responseContent);

                        if (currentlyPlaying != null && currentlyPlaying.is_playing)
                        {
                            string trackId = currentlyPlaying.item.id;
                            string trackName = currentlyPlaying.item.name;
                            string artistNames = string.Join(", ", currentlyPlaying.item.artists.ConvertAll(a => a.name));
                            return new CurrentlyPlayingTrack(trackId, trackName, artistNames);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Debug.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                //Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
        return null;
    }

    private async Task SaveTrackAsync()
    {
        try
        {
            var track = await GetCurrentlyPlayingTrack();
            if (track == null)
            {
                ShowNotification("Oops!", "No track is currently playing.");
                PlaySound(_ErrorSound);
                return;
            }

            await RefreshTokenIfNeededAsync();
            using (var httpClient = new HttpClient())
            {
                // Add authorization header
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var saveResponse = await httpClient.PutAsync($"https://api.spotify.com/v1/me/tracks?ids={track.trackId}", null);

                if (saveResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await RefreshAccessTokenAsync();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    saveResponse = await httpClient.PutAsync($"https://api.spotify.com/v1/me/tracks?ids={track.trackId}", null);
                }

                if (saveResponse.IsSuccessStatusCode)
                {
                    //Debug.WriteLine($"Successfully saved '{trackName}' by {artistNames} to your library!");
                    ShowNotification($"Liked Song", $"'{track.trackName}' by {track.artistNames}");
                    PlaySound(_LikeSongSound);
                }
                else
                {
                    //Debug.WriteLine($"Failed to save track. Status code: {saveResponse.StatusCode}");
                    ShowNotification("Oops!", "Failed to save track to Spotify.");
                    PlaySound(_ErrorSound);
                    //string errorContent = await saveResponse.Content.ReadAsStringAsync();
                    //Debug.WriteLine($"Error: {errorContent}");
                }
            }
        }
        catch (Exception ex)
        {
            //Debug.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                ShowNotification("Oops!", "Failed to save track to Spotify.");
                PlaySound(_ErrorSound);
                //Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }

    private async Task RemoveTrackAsync()
    {
        try
        {
            var track = await GetCurrentlyPlayingTrack();
            if (track == null)
            {
                ShowNotification("Oops!", "No track is currently playing.");
                PlaySound(_ErrorSound);
                return;
            }

            await RefreshTokenIfNeededAsync();
            using (var httpClient = new HttpClient())
            {
                // Add authorization header
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var saveResponse = await httpClient.DeleteAsync($"https://api.spotify.com/v1/me/tracks?ids={track.trackId}");

                if (saveResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await RefreshAccessTokenAsync();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    saveResponse = await httpClient.DeleteAsync($"https://api.spotify.com/v1/me/tracks?ids={track.trackId}");
                }

                if (saveResponse.IsSuccessStatusCode)
                {
                    ShowNotification($"Unliked Song", $"'{track.trackName}' by {track.artistNames}");
                    PlaySound(_UnlikeSongSound);
                }
                else
                {
                    //Debug.WriteLine($"Failed to save track. Status code: {saveResponse.StatusCode}");
                    ShowNotification("Oops!", "Failed to remove track from Spotify.");
                    PlaySound(_ErrorSound);
                    //string errorContent = await saveResponse.Content.ReadAsStringAsync();
                    //Debug.WriteLine($"Error: {errorContent}");
                }
            }
        }
        catch (Exception ex)
        {
            //Debug.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                ShowNotification("Oops!", "Failed to remove track from Spotify.");
                PlaySound(_ErrorSound);
                //Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }

    private async Task RefreshTokenIfNeededAsync()
    {
        if (_tokenExpirationTime <= DateTime.UtcNow.AddMinutes(5)) // 5-minute buffer
        {
            await RefreshAccessTokenAsync();
        }
    }

    private async Task RefreshAccessTokenAsync()
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var requestBody = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", _refreshToken)
            });

                var response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(content);

                    _accessToken = tokenResponse.access_token;
                    _tokenExpirationTime = DateTime.UtcNow.AddSeconds(tokenResponse.expires_in);
                    _refreshToken = tokenResponse.refresh_token;

                    SpotifyLikeButtonSettings.Default.AccessToken = _accessToken;
                    SpotifyLikeButtonSettings.Default.TokenExpirationTime = _tokenExpirationTime;
                    SpotifyLikeButtonSettings.Default.RefreshToken = _refreshToken;
                    SpotifyLikeButtonSettings.Default.Save();
                }
                else
                {
                    //Debug.WriteLine($"Failed to refresh token. Status code: {response.StatusCode}");
                    //Debug.WriteLine($"Error: {content}");
                    ShowNotification("Oops!", "Failed to refresh Spotify token.");
                    PlaySound(_ErrorSound);
                }
            }
        }
        catch (Exception ex)
        {
            //Debug.WriteLine($"Error refreshing token: {ex.Message}");
        }
    }

    // Show a notification to the user
    private void ShowNotification(string title, string message)
    {
        if (SpotifyLikeButtonSettings.Default.ShowNotifications == false)
        {
            return;
        }

        Icon SpotifyLikeButtonIcon;
        try { 
            SpotifyLikeButtonIcon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.ico"));
        }
        catch {
            SpotifyLikeButtonIcon = SystemIcons.Information;
        }

        var notification = new NotifyIcon
        {
            Visible = true,
            Icon = SpotifyLikeButtonIcon,
            BalloonTipTitle = title,
            BalloonTipText = message
        };

        notification.ShowBalloonTip(3000); // Show for 3 seconds

        // Clean up the notification icon after a delay
        System.Threading.Timer timer = null;
        timer = new System.Threading.Timer((obj) =>
        {
            notification.Dispose();
            timer.Dispose();
        }, null, 3000, System.Threading.Timeout.Infinite);
    }
}


// Classes for deserializing the Spotify API responses
public class CurrentlyPlayingResponse
{
    public bool is_playing { get; set; }
    public TrackItem item { get; set; }
}

public class TrackItem
{
    public string id { get; set; }
    public string name { get; set; }
    public List<Artist> artists { get; set; }
}

public class Artist
{
    public string id { get; set; }
    public string name { get; set; }
}

public class CurrentlyPlayingTrack
{
    public string trackId { get; set; }
    public string trackName { get; set; }
    public string artistNames { get; set; }
    public CurrentlyPlayingTrack(string trackId, string trackName, string artistNames)
    {
        this.trackId = trackId;
        this.trackName = trackName;
        this.artistNames = artistNames;
    }
}