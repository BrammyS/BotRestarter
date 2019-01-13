using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BotRestarter
{
    public class Program
    {

        private static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {
            var botFilePaths = GetFiles();
            Log(TimeSpan.FromMilliseconds(int.MaxValue).TotalDays.ToString());
            if (!botFilePaths.Any())
            {
                await ShowErrorMessageAsync("No bots where found!").ConfigureAwait(false);
                return;
            } 

            // Loop trough all the bots and start them.
            foreach (var bot in botFilePaths)
            {
                var thread = new Thread(() => StartBot(bot));
                thread.Start();
            }

            await Task.Delay(-1).ConfigureAwait(false);
        }


        /// <summary>
        /// Starts a bot with the provided file path.
        /// And it keeps restarting the bot if it closes.
        /// </summary>
        /// <param name="botFilePath">The filepath of the bot that will be started.</param>
        private void StartBot(string botFilePath)
        {

            while (true)
            {
                var botProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = false,
                        WindowStyle = ProcessWindowStyle.Minimized,
                        FileName = botFilePath,
                        UseShellExecute = true
                    }
                };
                botProcess.Start();
                Log($"{botFilePath} opened.");
                botProcess.WaitForExit();
                Log($"{botFilePath} closed.");
            }
        }


        /// <summary>
        /// Loads all the bot file paths in the bots folder.
        /// </summary>
        /// <returns>
        /// A array of strings containing the file paths.
        /// </returns>
        private string[] GetFiles()
        {
            var botFilePaths = Directory.GetFiles("bots", "*.lnk", SearchOption.AllDirectories);
            foreach (var file in botFilePaths) Log($"Found {file}!");
            return botFilePaths;
        }


        /// <summary>
        /// Logs a message to the console with a red color.
        /// </summary>
        /// <param name="message">The message that will be printed to the console.</param>
        private async Task ShowErrorMessageAsync(string message)
        {
            Log(message, ConsoleColor.Red);
            await Task.Delay(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
        }


        /// <summary>
        /// Logs a message to the console.
        /// The <paramref name="color"/> is by default <see cref="ConsoleColor.Gray"/>
        /// </summary>
        /// <param name="message">The message that will be printed to the console.</param>
        /// <param name="color">The color that the message will have when printing something to the console.</param>
        private void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} : " + message);
            Console.ResetColor();
        }
    }
}
