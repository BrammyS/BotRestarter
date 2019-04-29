using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotRestarter.Interfaces;
using BotRestarter.Logger.Commands;
using BotRestarter.Logger.Interfaces;
using BotRestarter.Timers;

namespace BotRestarter
{
    public class BotRestarter : IBotRestarter
    {
        private readonly ILogger _logger;
        private readonly ConsoleReSetterTimer _consoleReSetterTimer;
        private readonly IBotReader _botReader;
        private readonly IConsoleCommands _commands;


        /// <summary>
        /// Creates a new <see cref="BotRestarter"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that will be used to log messages to the console.</param>
        /// <param name="consoleReSetterTimer">The <see cref="ConsoleReSetterTimer"/> that will be used.</param>
        /// <param name="botReader">The <see cref="IBotReader"/> that will be used to load all the bots and check for should restart values.</param>
        /// <param name="commands">The <see cref="IConsoleCommands"/> that will be used.</param>
        public BotRestarter(ILogger logger, ConsoleReSetterTimer consoleReSetterTimer, IBotReader botReader, IConsoleCommands commands)
        {
            _logger = logger;
            _consoleReSetterTimer = consoleReSetterTimer;
            _botReader = botReader;
            _commands = commands;
        }


        /// <inheritdoc />
        public async Task StartAsync()
        {
            var botFilePaths = _botReader.GetBotFiles();
            if (!botFilePaths.Any()) ShowErrorMessage("No bots where found!");

            // Loop trough all the bots and start them.
            foreach (var bot in botFilePaths)
            {
                _botReader.StoreBot(bot, true);
                var thread = new Thread(async () => await StartBotAsync(bot).ConfigureAwait(false));
                thread.Start();
            }

            await StartTimers().ConfigureAwait(false);

            while (true)
            {
                var userInput = Console.ReadLine(); 
                if(userInput == "exit") break;
                if (userInput == null) continue;
                switch (userInput.ToLower())
                {
                    case "bots":
                        _commands.BotsCommand();
                        break;
                }
            }
        }


        /// <summary>
        /// Starts a bot with the provided file path.
        /// And it keeps restarting the bot if it closes.
        /// </summary>
        /// <param name="botFileName">The filepath of the bot that will be started.</param>
        private async Task StartBotAsync(string botFileName)
        {
            var botProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    FileName = botFileName,
                    UseShellExecute = true
                }
            };

            // Keep restating the bot if it closes.
            while (true)
            {
                if (_botReader.GetShouldRestartBot(botFileName))
                {
                    botProcess.Start();
                    _logger.Log($"{botFileName} opened.");
                    botProcess.WaitForExit();
                    _logger.Log($"{botFileName} closed.");
                }
                else await Task.Delay(TimeSpan.FromSeconds(1.5)).ConfigureAwait(false);
            }
            // ReSharper disable once FunctionNeverReturns
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
