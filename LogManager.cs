using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLikeButton
{
    public class LogManager
    {
        private static string GetLogFilePath()
        {
            try
            {
                //string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpotifyLikeButton", "Logs");
                string logFileName = $"SpotifyLikeButton_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.log";
                return Path.Combine(logDirectory, logFileName);
            }
            catch (Exception ex)
            {
                // Fallback to a simple path if there are issues with base directory
                MessageBox.Show($"Error creating log path: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"SpotifyLikeButton_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.log");
            }
        }
    
        private static void EnsureLogDirectoryExists(string logPath)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not create log directory: {ex.Message}", "Logging Directory Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void ConfigureTraceListener(bool enable)
        {
            try
            {
                // Remove existing listener if it exists
                var existingListener = Trace.Listeners["spotifyLikeButtonListener"] as TextWriterTraceListener;
                if (existingListener != null)
                {
                    Trace.Listeners.Remove(existingListener);
                    existingListener.Close();
                }

                if (enable)
                {
                    string logPath = GetLogFilePath();
                    EnsureLogDirectoryExists(logPath);

                    var listener = new TextWriterTraceListener(logPath, "spotifyLikeButtonListener");
                    Trace.Listeners.Add(listener);
                    Trace.AutoFlush = true;

                    // Log with timestamp
                    WriteLog($"Logging enabled. Log file: {logPath}");
                }
                else
                {
                    WriteLog("Logging disabled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring trace listener: {ex.Message}", "Logging Configuration Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void WriteLog(string message)
        {
            try
            {
                if (!SpotifyLikeButtonSettings.Default.EnableLogging)
                {
                    return;
                }
                // Format: [Timestamp] Message
                string timestampedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}";
                Trace.WriteLine(timestampedMessage);
            }
            catch (Exception ex)
            {
                // Fallback error logging
                System.Diagnostics.Debug.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
