using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks.Dataflow;
using static System.Net.Mime.MediaTypeNames;

namespace SpotifyLikeButton
{
    public partial class SettingsForm : Form
    {
        private const string StartupFolderName = "SpotifyLikeButton";
        private const string StartupRegistryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private Keys modifiers = Keys.None;
        private Keys mainKey = Keys.None;
        private bool isListening = false;
        private Label activeLabel;


        public SettingsForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(".\\icon.ico");
            this.KeyDown += SettingsForm_KeyDown;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            lblLikeHotkey.Text = SpotifyLikeButtonSettings.Default.LikeSongHotkey;
            lblUnlikeHotkey.Text = SpotifyLikeButtonSettings.Default.UnlikeSongHotkey;
            PopulateComboBoxes();

            if (SpotifyLikeButtonSettings.Default.LaunchOnStartup)
            {
                btnChangeStartupStatus.Text = "Disable";
                lblStartupStatus.Text = "Enabled";
            }
            else
            {
                btnChangeStartupStatus.Text = "Enable";
                lblStartupStatus.Text = "Disabled";
            }

            if (SpotifyLikeButtonSettings.Default.ShowNotifications)
            {
                btnChangeNotificationStatus.Text = "Disable";
                lblNotificationStatus.Text = "Enabled";
            }
            else
            {
                btnChangeNotificationStatus.Text = "Enable";
                lblNotificationStatus.Text = "Disabled";
            }

            if (SpotifyLikeButtonSettings.Default.EnableLogging)
            {
                btnChangeLoggingStatus.Text = "Disable";
                lblLoggingStatus.Text = "Enabled";
            }
            else
            {
                btnChangeLoggingStatus.Text = "Enable";
                lblLoggingStatus.Text = "Disabled";
            }
        }

        private void PopulateComboBoxes()
        {
            ComboBoxItem cbEmptyItem = new ComboBoxItem("<NONE>", "");
            cbLikeSongSound.Items.Add(cbEmptyItem);
            cbUnlikeSongSound.Items.Add(cbEmptyItem);
            cbErrorSound.Items.Add(cbEmptyItem);
            if ("<NONE>" == SpotifyLikeButtonSettings.Default.LikeSongSound)
            {
                cbLikeSongSound.SelectedIndex = 0;
            }
            if ("<NONE>" == SpotifyLikeButtonSettings.Default.UnlikeSongSound)
            {
                cbUnlikeSongSound.SelectedIndex = 0;
            }
            if ("<NONE>" == SpotifyLikeButtonSettings.Default.ErrorSound)
            {
                cbErrorSound.SelectedIndex = 0;
            }

            string notificationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NotificationSounds");
            if (Directory.Exists(notificationDirectory))
            {
                string[] files = Directory.GetFiles("./NotificationSounds", "*.wav");
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(files[i]);
                    ComboBoxItem cbItem = new ComboBoxItem(fileName, files[i]);
                    cbLikeSongSound.Items.Add(cbItem);
                    cbUnlikeSongSound.Items.Add(cbItem);
                    cbErrorSound.Items.Add(cbItem);

                    if (files[i] == SpotifyLikeButtonSettings.Default.LikeSongSound)
                    {
                        cbLikeSongSound.SelectedIndex = i + 1;
                    }
                    if (files[i] == SpotifyLikeButtonSettings.Default.UnlikeSongSound)
                    {
                        cbUnlikeSongSound.SelectedIndex = i + 1;
                    }
                    if (files[i] == SpotifyLikeButtonSettings.Default.ErrorSound)
                    {
                        cbErrorSound.SelectedIndex = i + 1;
                    }
                }
            }

            if (cbLikeSongSound.SelectedIndex == -1)
            {
                cbLikeSongSound.SelectedIndex = 0;
            }
            if (cbUnlikeSongSound.SelectedIndex == -1)
            {
                cbUnlikeSongSound.SelectedIndex = 0;
            }
            if (cbErrorSound.SelectedIndex == -1)
            {
                cbErrorSound.SelectedIndex = 0;
            }
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isListening)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            string formattedShortcut = "";
            if (e.Control)
            {
                formattedShortcut += "Control";
            }

            if (e.Shift)
            {
                formattedShortcut = addPlus(formattedShortcut);
                formattedShortcut += "Shift";
            }

            if (e.Alt)
            {
                formattedShortcut = addPlus(formattedShortcut);
                formattedShortcut += "Alt";
            }

            // Capture main key (ignore if it's just a modifier)
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Menu)
            {
                formattedShortcut = addPlus(formattedShortcut);
                formattedShortcut += e.KeyCode.ToString();
                if (activeLabel == lblLikeHotkey)
                {
                    SpotifyLikeButtonSettings.Default.LikeSongHotkey = formattedShortcut;
                    btnEditLike_Click(null, null);
                }
                else if (activeLabel == lblUnlikeHotkey)
                {
                    SpotifyLikeButtonSettings.Default.UnlikeSongHotkey = formattedShortcut;
                    btnEditUnlike_Click(null, null);
                }

                SpotifyLikeButtonSettings.Default.Save();

                if (Program.hotKeyManager != null)
                {
                    Program.hotKeyManager.LoadSettings();
                }
            }

            activeLabel.Text = formattedShortcut;
        }

        private string addPlus(string str)
        {
            if (str != "")
                str += "+";
            return str;
        }

        private void btnEditLike_Click(object sender, EventArgs e)
        {
            if (isListening)
            {
                lblLikeHotkey.Text = SpotifyLikeButtonSettings.Default.LikeSongHotkey;
                btnEditLike.Text = "Edit";
                btnEditUnlike.Enabled = true;
                isListening = false;
                this.KeyPreview = false;
            }
            else
            {
                lblLikeHotkey.Text = "Press key combo.";
                btnEditLike.Text = "Cancel";
                btnEditUnlike.Enabled = false;
                activeLabel = lblLikeHotkey;
                isListening = true;
                this.KeyPreview = true;
            }
        }

        private void btnEditUnlike_Click(object sender, EventArgs e)
        {
            if (isListening)
            {
                lblUnlikeHotkey.Text = SpotifyLikeButtonSettings.Default.UnlikeSongHotkey;
                btnEditUnlike.Text = "Edit";
                btnEditLike.Enabled = true;
                isListening = false;
                this.KeyPreview = false;
            }
            else
            {
                lblUnlikeHotkey.Text = "Press key combo.";
                btnEditUnlike.Text = "Cancel";
                btnEditLike.Enabled = false;
                activeLabel = lblUnlikeHotkey;
                isListening = true;
                this.KeyPreview = true;
            }
        }
        private void cbLikeSongSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpotifyLikeButtonSettings.Default.LikeSongSound = ((ComboBoxItem)cbLikeSongSound.SelectedItem).FilePath;
            SpotifyLikeButtonSettings.Default.Save();
            Program.hotKeyManager.LoadSettings();
        }

        private void cbUnlikeSongSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpotifyLikeButtonSettings.Default.UnlikeSongSound = ((ComboBoxItem)cbUnlikeSongSound.SelectedItem).FilePath;
            SpotifyLikeButtonSettings.Default.Save();
            Program.hotKeyManager.LoadSettings();
        }

        private void cbErrorSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpotifyLikeButtonSettings.Default.ErrorSound = ((ComboBoxItem)cbErrorSound.SelectedItem).FilePath;
            SpotifyLikeButtonSettings.Default.Save();
            Program.hotKeyManager.LoadSettings();
        }
        private void btnPlayLikeSound_Click(object sender, EventArgs e)
        {
            Program.hotKeyManager.PlaySound(((ComboBoxItem)cbLikeSongSound.SelectedItem).FilePath);
        }

        private void btnPlayUnikeSound_Click(object sender, EventArgs e)
        {
            Program.hotKeyManager.PlaySound(((ComboBoxItem)cbUnlikeSongSound.SelectedItem).FilePath);
        }

        private void btnPlayErrorSound_Click(object sender, EventArgs e)
        {
            Program.hotKeyManager.PlaySound(((ComboBoxItem)cbErrorSound.SelectedItem).FilePath);
        }

        private void btnChangeStartupStatus_Click(object sender, EventArgs e)
        {
            if (SpotifyLikeButtonSettings.Default.LaunchOnStartup)
            {
                DisableStartup();
                SpotifyLikeButtonSettings.Default.LaunchOnStartup = false;
                SpotifyLikeButtonSettings.Default.Save();
                btnChangeStartupStatus.Text = "Enable";
                lblStartupStatus.Text = "Disabled";
            }
            else
            {
                EnableStartup();
                SpotifyLikeButtonSettings.Default.LaunchOnStartup = true;
                SpotifyLikeButtonSettings.Default.Save();
                btnChangeStartupStatus.Text = "Disable";
                lblStartupStatus.Text = "Enabled";
            }
        }

        private void EnableStartup()
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "SpotifyLikeButton.lnk");

            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); //Windows Script Host Shell Object
            dynamic shell = Activator.CreateInstance(t);
            try
            {
                var lnk = shell.CreateShortcut(shortcutPath);
                try
                {
                    lnk.TargetPath = System.Windows.Forms.Application.ExecutablePath.Replace("\\", "/");
                    lnk.IconLocation = System.Windows.Forms.Application.ExecutablePath;
                    lnk.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
                    lnk.Save();
                }
                finally
                {
                    Marshal.FinalReleaseComObject(lnk);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(shell);
            }


            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupRegistryKey, true))
            {
                key.SetValue(StartupFolderName, System.Windows.Forms.Application.ExecutablePath);
            }
        }

        private void DisableStartup()
        {
            // Remove startup folder shortcut
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "SpotifyLikeButton.lnk");

            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
            }

            // Remove from registry
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupRegistryKey, true))
            {
                key.DeleteValue(StartupFolderName, false);
            }
        }

        private void btnChangeNotificationStatus_Click(object sender, EventArgs e)
        {
            if (SpotifyLikeButtonSettings.Default.ShowNotifications)
            {
                SpotifyLikeButtonSettings.Default.ShowNotifications = false;
                SpotifyLikeButtonSettings.Default.Save();
                btnChangeNotificationStatus.Text = "Enable";
                lblNotificationStatus.Text = "Disabled";
            }
            else
            {
                SpotifyLikeButtonSettings.Default.ShowNotifications = true;
                SpotifyLikeButtonSettings.Default.Save();
                btnChangeNotificationStatus.Text = "Disable";
                lblNotificationStatus.Text = "Enabled";
            }
        }

        private void btnChangeLoggingStatus_Click(object sender, EventArgs e)
        {
            if (SpotifyLikeButtonSettings.Default.EnableLogging)
            {
                LogManager.WriteLog("Logging disabled");
                LogManager.ConfigureTraceListener(false);
                SpotifyLikeButtonSettings.Default.EnableLogging = false;                
                SpotifyLikeButtonSettings.Default.Save();
                btnChangeLoggingStatus.Text = "Enable";
                lblLoggingStatus.Text = "Disabled";
            }
            else
            {
                LogManager.ConfigureTraceListener(true);
                SpotifyLikeButtonSettings.Default.EnableLogging = true;
                SpotifyLikeButtonSettings.Default.Save();
                LogManager.WriteLog("Logging enabled");
                btnChangeLoggingStatus.Text = "Disable";
                lblLoggingStatus.Text = "Enabled";                
            }
        }
    }

    public class ComboBoxItem
    {
        public string DisplayText { get; }
        public string FilePath { get; }

        public ComboBoxItem(string displayText, string filePath)
        {
            DisplayText = displayText;
            FilePath = filePath;
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}