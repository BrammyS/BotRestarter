using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotRestarter.Interfaces;
using BotRestarter.Logger.Interfaces;
using BotRestarter.Timers;

namespace BotRestarter
{
    public class BotRestarter : IBotRestarter
    {
        private readonly ILogger _logger;
        private readonly ConsoleReSetterTimer _consoleReSetterTimer;


        /// <summary>
        /// Creates a new <see cref="BotRestarter"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that will be used to log messages to the console.</param>
        /// <param name="consoleReSetterTimer">The <see cref="ConsoleReSetterTimer"/> that will be used.</param>
        public BotRestarter(ILogger logger, ConsoleReSetterTimer consoleReSetterTimer)
        {
            _logger = logger;
            _consoleReSetterTimer = consoleReSetterTimer;
        }

        /// <inheritdoc />
        public async Task StartAsync()
        {
            var botFilePaths = GetFiles();
            if (!botFilePaths.Any()) ShowErrorMessage("No bots where found!");

            // Loop trough all the bots and start them.
            foreach (var bot in botFilePaths)
            {
                var thread = new Thread(() => StartBot(bot));
                thread.Start();
            }

            await StartTimers().ConfigureAwait(false);

            // Await the task so the program doesn't close. 
            await Task.Delay(-1).ConfigureAwait(false);
        }


        /// <summary>
        /// Starts a bot with the provided file path.
        /// And it keeps restarting the bot if it closes.
        /// </summary>
        /// <param name="botFilePath">The filepath of the bot that will be started.</param>
        private void StartBot(string botFilePath)
        {

            // Keep restating the bot if it closes.
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
                _logger.Log($"{botFilePath} opened.");
                botProcess.WaitForExit();
                _logger.Log($"{botFilePath} closed.");
            }
            // ReSharper disable once FunctionNeverReturns
        }


        /// <summary>
        /// Loads all the bot file paths in the bots folder.
        /// </summary>
        /// <returns>
        /// A array of strings containing the file paths.
        /// </returns>
        private string[] GetFiles()
        {

            // If Bots directory doesn't exist, create one.
            if (!Directory.Exists("Bots")) Directory.CreateDirectory("Bots");

            // Load all the bot shortcuts in the Bots folder.
            var botFilePaths = Directory.GetFiles("bots", "*.lnk", SearchOption.AllDirectories);
            foreach (var file in botFilePaths) _logger.Log($"Found {file}!");
            return botFilePaths;
        }


        /// <summary>
        /// Starts all the timers.
        /// </summary>
        private async Task StartTimers()
        {
            await _consoleReSetterTimer.TimerAsync().ConfigureAwait(false);
        }



        /// <summary>
        /// Logs a message to the console with a red color.
        /// </summary>
        /// <param name="message">The message that will be printed to the console.</param>
        private void ShowErrorMessage(string message) => _logger.Log(message, ConsoleColor.Red);
    }
}
