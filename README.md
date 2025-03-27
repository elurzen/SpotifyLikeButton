# SpotifyLikeButton
 The SpotifyLikeButton app allows you to Like/Unlike songs without interrupting the flow of whatever you're doing - working, gaming, watching videos, whatever it may be.

 Download the SpotifyLikeButtonSetup.msi from the Releases section and install the app. 

 After it installs, use the shortcut on your desktop or navigate to where you installed the app to run the SpotifyLikeButton.exe.

 A green plus icon will appear in your icon tray (click the arrow if you don't see it). You can double-click the icon or right-click > select "Settings" to open the settings menu. 

<img width="57" alt="SpotifyLikeButtonTrayIcon" src="https://github.com/user-attachments/assets/b05e2d01-efdc-4731-a6f6-6f4b01e90405" />
 
<img width="440" alt="SpotifyLikeButtonSettings" src="https://github.com/user-attachments/assets/faf8d2af-6e41-4f5b-8a60-a61e5e71dc7b" />


 By default, you can Like songs with F4 and Unlike them with F8. Pressing "Like" when a song is already liked won't unlike it, you need to deliberately press the "Unlike" key.

 If you choose to edit these keys, you can press any combination of Ctrl, Alt, and Shift (or none of them) plus a trigger key. When you press your trigger key the combination will automatically save. 

 The 3 sound drop downs in the middle can be previewed with the play button next to them. These sounds will play when Spotify confirms it completed the action successfully, if it fails, you'll receive the error sound. These can be set to < NONE > if you don't want any sound indication.

 You can add custom sounds by adding .wav files to the "NotificationSounds" folder; they will appear in the dropdowns the next time you open the settings menu.
 
 "Run at Startup" is self-explanatory and disabled by default. 
 
 "Show Notifications" will display a Windows notification with the outcome of the action; it includes the song title and artist(s) as well. This is also disabled by default.

 "Enable Logging" should be left disabled unless the logs are needed for troubleshooting with a developer. They are stored at C:\Users\[USERNAME]\AppData\Local\SpotifyLikeButton\Logs


# The Spotify desktop and web players don't always update right away. If you receive a success sound or notification, it worked. 
