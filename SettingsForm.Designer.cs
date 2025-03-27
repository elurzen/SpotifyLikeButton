using System.Configuration;

namespace SpotifyLikeButton
{
    partial class SettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        //private void LoadSettings()
        //{
        //    txtSaveHotkey.Text = ConfigurationManager.AppSettings["SaveHotkey"] ?? "Ctrl+0";
        //    txtRemoveHotkey.Text = ConfigurationManager.AppSettings["RemoveHotkey"] ?? "Ctrl+G4";
        //    txtSaveSound.Text = ConfigurationManager.AppSettings["SaveSound"] ?? "C:\\Windows\\Media\\chimes.wav";
        //    txtRemoveSound.Text = ConfigurationManager.AppSettings["RemoveSound"] ?? "";

        //    //ConfigurationManager.AppSettings["SaveHotkey"] = txtSaveHotkey.Text;
        //}

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Label lblUnlikeSong;
            Label lblUnlikeSound;
            Label lblErrorSound;
            Label lblRunAtStartup;
            Label lblShowNotifications;
            Label lblEnableLogging;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            lblLikeSong = new Label();
            lblLikeSound = new Label();
            lblLikeHotkey = new Label();
            lblUnlikeHotkey = new Label();
            cbLikeSongSound = new ComboBox();
            cbUnlikeSongSound = new ComboBox();
            cbErrorSound = new ComboBox();
            btnEditLike = new Button();
            btnEditUnlike = new Button();
            btnPlayLikeSound = new Button();
            btnPlayUnikeSound = new Button();
            btnPlayErrorSound = new Button();
            lblStartupStatus = new Label();
            btnChangeStartupStatus = new Button();
            btnChangeNotificationStatus = new Button();
            lblNotificationStatus = new Label();
            btnChangeLoggingStatus = new Button();
            lblLoggingStatus = new Label();
            lblUnlikeSong = new Label();
            lblUnlikeSound = new Label();
            lblErrorSound = new Label();
            lblRunAtStartup = new Label();
            lblShowNotifications = new Label();
            lblEnableLogging = new Label();
            SuspendLayout();
            // 
            // lblUnlikeSong
            // 
            lblUnlikeSong.AutoSize = true;
            lblUnlikeSong.BackColor = Color.Transparent;
            lblUnlikeSong.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblUnlikeSong.ForeColor = Color.FromArgb(51, 204, 102);
            lblUnlikeSong.Location = new Point(28, 67);
            lblUnlikeSong.Margin = new Padding(2, 0, 2, 0);
            lblUnlikeSong.Name = "lblUnlikeSong";
            lblUnlikeSong.Size = new Size(160, 18);
            lblUnlikeSong.TabIndex = 1;
            lblUnlikeSong.Text = "Unlike Song Hotkey:";
            // 
            // lblUnlikeSound
            // 
            lblUnlikeSound.AutoSize = true;
            lblUnlikeSound.BackColor = Color.Transparent;
            lblUnlikeSound.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblUnlikeSound.ForeColor = Color.FromArgb(51, 204, 102);
            lblUnlikeSound.Location = new Point(28, 149);
            lblUnlikeSound.Margin = new Padding(2, 0, 2, 0);
            lblUnlikeSound.Name = "lblUnlikeSound";
            lblUnlikeSound.Size = new Size(152, 18);
            lblUnlikeSound.TabIndex = 3;
            lblUnlikeSound.Text = "Unlike Song Sound:";
            // 
            // lblErrorSound
            // 
            lblErrorSound.AutoSize = true;
            lblErrorSound.BackColor = Color.Transparent;
            lblErrorSound.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblErrorSound.ForeColor = Color.FromArgb(51, 204, 102);
            lblErrorSound.Location = new Point(28, 190);
            lblErrorSound.Margin = new Padding(2, 0, 2, 0);
            lblErrorSound.Name = "lblErrorSound";
            lblErrorSound.Size = new Size(104, 18);
            lblErrorSound.TabIndex = 4;
            lblErrorSound.Text = "Error Sound:";
            // 
            // lblRunAtStartup
            // 
            lblRunAtStartup.AutoSize = true;
            lblRunAtStartup.BackColor = Color.Transparent;
            lblRunAtStartup.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblRunAtStartup.ForeColor = Color.FromArgb(51, 204, 102);
            lblRunAtStartup.Location = new Point(28, 231);
            lblRunAtStartup.Margin = new Padding(2, 0, 2, 0);
            lblRunAtStartup.Name = "lblRunAtStartup";
            lblRunAtStartup.Size = new Size(128, 18);
            lblRunAtStartup.TabIndex = 15;
            lblRunAtStartup.Text = "Run at Startup:";
            // 
            // lblShowNotifications
            // 
            lblShowNotifications.AutoSize = true;
            lblShowNotifications.BackColor = Color.Transparent;
            lblShowNotifications.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblShowNotifications.ForeColor = Color.FromArgb(51, 204, 102);
            lblShowNotifications.Location = new Point(28, 272);
            lblShowNotifications.Margin = new Padding(2, 0, 2, 0);
            lblShowNotifications.Name = "lblShowNotifications";
            lblShowNotifications.Size = new Size(160, 18);
            lblShowNotifications.TabIndex = 18;
            lblShowNotifications.Text = "Show Notifications:";
            // 
            // lblEnableLogging
            // 
            lblEnableLogging.AutoSize = true;
            lblEnableLogging.BackColor = Color.Transparent;
            lblEnableLogging.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblEnableLogging.ForeColor = Color.FromArgb(51, 204, 102);
            lblEnableLogging.Location = new Point(28, 313);
            lblEnableLogging.Margin = new Padding(2, 0, 2, 0);
            lblEnableLogging.Name = "lblEnableLogging";
            lblEnableLogging.Size = new Size(128, 18);
            lblEnableLogging.TabIndex = 21;
            lblEnableLogging.Text = "Enable Logging:";
            // 
            // lblLikeSong
            // 
            lblLikeSong.AutoSize = true;
            lblLikeSong.BackColor = Color.Transparent;
            lblLikeSong.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLikeSong.ForeColor = Color.FromArgb(51, 204, 102);
            lblLikeSong.Location = new Point(28, 26);
            lblLikeSong.Margin = new Padding(2, 0, 2, 0);
            lblLikeSong.Name = "lblLikeSong";
            lblLikeSong.Size = new Size(152, 18);
            lblLikeSong.TabIndex = 0;
            lblLikeSong.Text = "Like Song Hotkey: ";
            // 
            // lblLikeSound
            // 
            lblLikeSound.AutoSize = true;
            lblLikeSound.BackColor = Color.Transparent;
            lblLikeSound.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLikeSound.ForeColor = Color.FromArgb(51, 204, 102);
            lblLikeSound.Location = new Point(28, 108);
            lblLikeSound.Margin = new Padding(2, 0, 2, 0);
            lblLikeSound.Name = "lblLikeSound";
            lblLikeSound.Size = new Size(136, 18);
            lblLikeSound.TabIndex = 2;
            lblLikeSound.Text = "Like Song Sound:";
            // 
            // lblLikeHotkey
            // 
            lblLikeHotkey.AutoSize = true;
            lblLikeHotkey.BackColor = Color.Transparent;
            lblLikeHotkey.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLikeHotkey.ForeColor = Color.FromArgb(51, 204, 102);
            lblLikeHotkey.Location = new Point(230, 26);
            lblLikeHotkey.Margin = new Padding(2, 0, 2, 0);
            lblLikeHotkey.Name = "lblLikeHotkey";
            lblLikeHotkey.Size = new Size(88, 18);
            lblLikeHotkey.TabIndex = 5;
            lblLikeHotkey.Text = "LikeHotkey";
            // 
            // lblUnlikeHotkey
            // 
            lblUnlikeHotkey.AutoSize = true;
            lblUnlikeHotkey.BackColor = Color.Transparent;
            lblUnlikeHotkey.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUnlikeHotkey.ForeColor = Color.FromArgb(51, 204, 102);
            lblUnlikeHotkey.Location = new Point(230, 67);
            lblUnlikeHotkey.Margin = new Padding(2, 0, 2, 0);
            lblUnlikeHotkey.Name = "lblUnlikeHotkey";
            lblUnlikeHotkey.Size = new Size(104, 18);
            lblUnlikeHotkey.TabIndex = 6;
            lblUnlikeHotkey.Text = "UnlikeHotkey";
            // 
            // cbLikeSongSound
            // 
            cbLikeSongSound.BackColor = SystemColors.ControlText;
            cbLikeSongSound.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLikeSongSound.FlatStyle = FlatStyle.Flat;
            cbLikeSongSound.Font = new Font("Consolas", 11F, FontStyle.Bold);
            cbLikeSongSound.ForeColor = Color.FromArgb(51, 204, 102);
            cbLikeSongSound.FormattingEnabled = true;
            cbLikeSongSound.Location = new Point(233, 104);
            cbLikeSongSound.Margin = new Padding(2);
            cbLikeSongSound.Name = "cbLikeSongSound";
            cbLikeSongSound.Size = new Size(284, 26);
            cbLikeSongSound.TabIndex = 7;
            cbLikeSongSound.SelectedIndexChanged += cbLikeSongSound_SelectedIndexChanged;
            // 
            // cbUnlikeSongSound
            // 
            cbUnlikeSongSound.BackColor = SystemColors.ControlText;
            cbUnlikeSongSound.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUnlikeSongSound.FlatStyle = FlatStyle.Flat;
            cbUnlikeSongSound.Font = new Font("Consolas", 11F, FontStyle.Bold);
            cbUnlikeSongSound.ForeColor = Color.FromArgb(51, 204, 102);
            cbUnlikeSongSound.FormattingEnabled = true;
            cbUnlikeSongSound.Location = new Point(233, 145);
            cbUnlikeSongSound.Margin = new Padding(2);
            cbUnlikeSongSound.Name = "cbUnlikeSongSound";
            cbUnlikeSongSound.Size = new Size(284, 26);
            cbUnlikeSongSound.TabIndex = 8;
            cbUnlikeSongSound.SelectedIndexChanged += cbUnlikeSongSound_SelectedIndexChanged;
            // 
            // cbErrorSound
            // 
            cbErrorSound.BackColor = SystemColors.ControlText;
            cbErrorSound.DropDownStyle = ComboBoxStyle.DropDownList;
            cbErrorSound.FlatStyle = FlatStyle.Flat;
            cbErrorSound.Font = new Font("Consolas", 11F, FontStyle.Bold);
            cbErrorSound.ForeColor = Color.FromArgb(51, 204, 102);
            cbErrorSound.FormattingEnabled = true;
            cbErrorSound.Location = new Point(233, 186);
            cbErrorSound.Margin = new Padding(2);
            cbErrorSound.Name = "cbErrorSound";
            cbErrorSound.Size = new Size(284, 26);
            cbErrorSound.TabIndex = 9;
            cbErrorSound.SelectedIndexChanged += cbErrorSound_SelectedIndexChanged;
            // 
            // btnEditLike
            // 
            btnEditLike.BackColor = SystemColors.ActiveCaptionText;
            btnEditLike.Font = new Font("Consolas", 11F, FontStyle.Bold);
            btnEditLike.ForeColor = Color.FromArgb(51, 204, 102);
            btnEditLike.Location = new Point(474, 19);
            btnEditLike.Margin = new Padding(2);
            btnEditLike.Name = "btnEditLike";
            btnEditLike.Size = new Size(88, 32);
            btnEditLike.TabIndex = 10;
            btnEditLike.Text = "Edit";
            btnEditLike.UseVisualStyleBackColor = false;
            btnEditLike.Click += btnEditLike_Click;
            // 
            // btnEditUnlike
            // 
            btnEditUnlike.BackColor = SystemColors.ActiveCaptionText;
            btnEditUnlike.Font = new Font("Consolas", 11F, FontStyle.Bold);
            btnEditUnlike.ForeColor = Color.FromArgb(51, 204, 102);
            btnEditUnlike.Location = new Point(474, 60);
            btnEditUnlike.Margin = new Padding(2);
            btnEditUnlike.Name = "btnEditUnlike";
            btnEditUnlike.Size = new Size(88, 32);
            btnEditUnlike.TabIndex = 11;
            btnEditUnlike.Text = "Edit";
            btnEditUnlike.UseVisualStyleBackColor = false;
            btnEditUnlike.Click += btnEditUnlike_Click;
            // 
            // btnPlayLikeSound
            // 
            btnPlayLikeSound.BackColor = SystemColors.ActiveCaptionText;
            btnPlayLikeSound.Font = new Font("Consolas", 15F, FontStyle.Bold);
            btnPlayLikeSound.ForeColor = Color.FromArgb(51, 204, 102);
            btnPlayLikeSound.Location = new Point(528, 102);
            btnPlayLikeSound.Margin = new Padding(0);
            btnPlayLikeSound.Name = "btnPlayLikeSound";
            btnPlayLikeSound.Size = new Size(34, 31);
            btnPlayLikeSound.TabIndex = 12;
            btnPlayLikeSound.Text = "►";
            btnPlayLikeSound.TextAlign = ContentAlignment.TopCenter;
            btnPlayLikeSound.UseVisualStyleBackColor = false;
            btnPlayLikeSound.Click += btnPlayLikeSound_Click;
            // 
            // btnPlayUnikeSound
            // 
            btnPlayUnikeSound.BackColor = SystemColors.ActiveCaptionText;
            btnPlayUnikeSound.Font = new Font("Consolas", 15F, FontStyle.Bold);
            btnPlayUnikeSound.ForeColor = Color.FromArgb(51, 204, 102);
            btnPlayUnikeSound.Location = new Point(528, 143);
            btnPlayUnikeSound.Margin = new Padding(0);
            btnPlayUnikeSound.Name = "btnPlayUnikeSound";
            btnPlayUnikeSound.Size = new Size(34, 31);
            btnPlayUnikeSound.TabIndex = 13;
            btnPlayUnikeSound.Text = "►";
            btnPlayUnikeSound.TextAlign = ContentAlignment.TopCenter;
            btnPlayUnikeSound.UseVisualStyleBackColor = false;
            btnPlayUnikeSound.Click += btnPlayUnikeSound_Click;
            // 
            // btnPlayErrorSound
            // 
            btnPlayErrorSound.BackColor = SystemColors.ActiveCaptionText;
            btnPlayErrorSound.Font = new Font("Consolas", 15F, FontStyle.Bold);
            btnPlayErrorSound.ForeColor = Color.FromArgb(51, 204, 102);
            btnPlayErrorSound.Location = new Point(528, 184);
            btnPlayErrorSound.Margin = new Padding(0);
            btnPlayErrorSound.Name = "btnPlayErrorSound";
            btnPlayErrorSound.Size = new Size(34, 31);
            btnPlayErrorSound.TabIndex = 14;
            btnPlayErrorSound.Text = "►";
            btnPlayErrorSound.TextAlign = ContentAlignment.TopCenter;
            btnPlayErrorSound.UseVisualStyleBackColor = false;
            btnPlayErrorSound.Click += btnPlayErrorSound_Click;
            // 
            // lblStartupStatus
            // 
            lblStartupStatus.AutoSize = true;
            lblStartupStatus.BackColor = Color.Transparent;
            lblStartupStatus.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStartupStatus.ForeColor = Color.FromArgb(51, 204, 102);
            lblStartupStatus.Location = new Point(233, 231);
            lblStartupStatus.Margin = new Padding(2, 0, 2, 0);
            lblStartupStatus.Name = "lblStartupStatus";
            lblStartupStatus.Size = new Size(72, 18);
            lblStartupStatus.TabIndex = 16;
            lblStartupStatus.Text = "Disabled";
            // 
            // btnChangeStartupStatus
            // 
            btnChangeStartupStatus.BackColor = SystemColors.ActiveCaptionText;
            btnChangeStartupStatus.Font = new Font("Consolas", 11F, FontStyle.Bold);
            btnChangeStartupStatus.ForeColor = Color.FromArgb(51, 204, 102);
            btnChangeStartupStatus.Location = new Point(474, 224);
            btnChangeStartupStatus.Margin = new Padding(2);
            btnChangeStartupStatus.Name = "btnChangeStartupStatus";
            btnChangeStartupStatus.Size = new Size(88, 32);
            btnChangeStartupStatus.TabIndex = 17;
            btnChangeStartupStatus.Text = "Enable";
            btnChangeStartupStatus.UseVisualStyleBackColor = false;
            btnChangeStartupStatus.Click += btnChangeStartupStatus_Click;
            // 
            // btnChangeNotificationStatus
            // 
            btnChangeNotificationStatus.BackColor = SystemColors.ActiveCaptionText;
            btnChangeNotificationStatus.Font = new Font("Consolas", 11F, FontStyle.Bold);
            btnChangeNotificationStatus.ForeColor = Color.FromArgb(51, 204, 102);
            btnChangeNotificationStatus.Location = new Point(474, 265);
            btnChangeNotificationStatus.Margin = new Padding(2);
            btnChangeNotificationStatus.Name = "btnChangeNotificationStatus";
            btnChangeNotificationStatus.Size = new Size(88, 32);
            btnChangeNotificationStatus.TabIndex = 20;
            btnChangeNotificationStatus.Text = "Enable";
            btnChangeNotificationStatus.UseVisualStyleBackColor = false;
            btnChangeNotificationStatus.Click += btnChangeNotificationStatus_Click;
            // 
            // lblNotificationStatus
            // 
            lblNotificationStatus.AutoSize = true;
            lblNotificationStatus.BackColor = Color.Transparent;
            lblNotificationStatus.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNotificationStatus.ForeColor = Color.FromArgb(51, 204, 102);
            lblNotificationStatus.Location = new Point(233, 272);
            lblNotificationStatus.Margin = new Padding(2, 0, 2, 0);
            lblNotificationStatus.Name = "lblNotificationStatus";
            lblNotificationStatus.Size = new Size(72, 18);
            lblNotificationStatus.TabIndex = 19;
            lblNotificationStatus.Text = "Disabled";
            // 
            // btnChangeLoggingStatus
            // 
            btnChangeLoggingStatus.BackColor = SystemColors.ActiveCaptionText;
            btnChangeLoggingStatus.Font = new Font("Consolas", 11F, FontStyle.Bold);
            btnChangeLoggingStatus.ForeColor = Color.FromArgb(51, 204, 102);
            btnChangeLoggingStatus.Location = new Point(474, 306);
            btnChangeLoggingStatus.Margin = new Padding(2);
            btnChangeLoggingStatus.Name = "btnChangeLoggingStatus";
            btnChangeLoggingStatus.Size = new Size(88, 32);
            btnChangeLoggingStatus.TabIndex = 23;
            btnChangeLoggingStatus.Text = "Enable";
            btnChangeLoggingStatus.UseVisualStyleBackColor = false;
            btnChangeLoggingStatus.Click += btnChangeLoggingStatus_Click;
            // 
            // lblLoggingStatus
            // 
            lblLoggingStatus.AutoSize = true;
            lblLoggingStatus.BackColor = Color.Transparent;
            lblLoggingStatus.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLoggingStatus.ForeColor = Color.FromArgb(51, 204, 102);
            lblLoggingStatus.Location = new Point(233, 313);
            lblLoggingStatus.Margin = new Padding(2, 0, 2, 0);
            lblLoggingStatus.Name = "lblLoggingStatus";
            lblLoggingStatus.Size = new Size(72, 18);
            lblLoggingStatus.TabIndex = 22;
            lblLoggingStatus.Text = "Disabled";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(588, 356);
            Controls.Add(btnChangeLoggingStatus);
            Controls.Add(lblLoggingStatus);
            Controls.Add(lblEnableLogging);
            Controls.Add(btnChangeNotificationStatus);
            Controls.Add(lblNotificationStatus);
            Controls.Add(lblShowNotifications);
            Controls.Add(btnChangeStartupStatus);
            Controls.Add(lblStartupStatus);
            Controls.Add(lblRunAtStartup);
            Controls.Add(btnPlayErrorSound);
            Controls.Add(btnPlayUnikeSound);
            Controls.Add(btnPlayLikeSound);
            Controls.Add(btnEditUnlike);
            Controls.Add(btnEditLike);
            Controls.Add(cbErrorSound);
            Controls.Add(cbUnlikeSongSound);
            Controls.Add(cbLikeSongSound);
            Controls.Add(lblUnlikeHotkey);
            Controls.Add(lblLikeHotkey);
            Controls.Add(lblErrorSound);
            Controls.Add(lblUnlikeSound);
            Controls.Add(lblLikeSound);
            Controls.Add(lblUnlikeSong);
            Controls.Add(lblLikeSong);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "SettingsForm";
            Text = "Spotify Like Button Settings";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLikeSong;
        private Label lblLikeSound;
        private Label lblLikeHotkey;
        private Label lblUnlikeHotkey;
        private ComboBox cbLikeSongSound;
        private ComboBox cbUnlikeSongSound;
        private ComboBox cbErrorSound;
        private Button btnEditLike;
        private Button btnEditUnlike;
        private Button btnPlayLikeSound;
        private Button btnPlayUnikeSound;
        private Button btnPlayErrorSound;
        private Label lblStartupStatus;
        private Button btnChangeStartupStatus;
        private Button btnChangeNotificationStatus;
        private Label lblNotificationStatus;
        private Button btnChangeLoggingStatus;
        private Label lblLoggingStatus;
    }
}
