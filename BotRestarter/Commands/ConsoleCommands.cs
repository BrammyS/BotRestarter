using System;
using BotRestarter.Interfaces;
using BotRestarter.Logger.Commands;
using BotRestarter.Logger.Interfaces;

namespace BotRestarter.Commands
{
    public class ConsoleCommands : IConsoleCommands
    {
        private readonly ILogger _logger;
        private readonly IBotReader _botReader;


        /// <summary>
        /// Creates a new <see cref="ConsoleCommands"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that will be used to log messages to the console.</param>
        /// <param name="botReader">The <see cref="IBotReader"/> that will be used to load all the bots and check for should restart values.</param>
        public ConsoleCommands(ILogger logger, IBotReader botReader)
        {
            _logger = logger;
            _botReader = botReader;
        }


        /// <inheritdoc />
        public void BotsCommand()
        {

            // Logging all the bots to the console.
            var bots = _botReader.GetAlBots();
            for (var i = 0; i < bots.Count; i++)
            {
                _logger.Log($"{i} | {bots[i].Key} | Should restart: `{bots[i].Value}`", ConsoleColor.Gray, false);
            }
            _logger.Log("Type the number of the bot where you want to change the restart settings. Type `c` to cancel.", ConsoleColor.Gray, false);



            // Keep running the bots command is the input wasn't correct.
            var hasCorrectInput = false;
            while (!hasCorrectInput)
            {
                var input = Console.ReadLine();

                // If input is c, stop the command.
                if (input != null && input.ToLower().Equals("c")) hasCorrectInput = true;

                // If input is a number, ask for the restart option.
                else if (input != null && int.TryParse(input.ToLower(), out var option))
                {
                    hasCorrectInput = true;
                    _logger.Log("Please select one of the following options for the auto restart.\r\n" +
                                "true or false. Type `c` to cancel.", ConsoleColor.Gray, false);

                    // Keep asking for the restart option if the input wasn't correct.
                    var hasCorrectSetting = false;
                    while (!hasCorrectSetting)
                    {
                        var settingInput = Console.ReadLine();

                        // If input is c, stop the command.
                        if (settingInput != null && settingInput.ToLower().Equals("c")) hasCorrectSetting = true;

                        // If input is a boolean, change for the previous chosen bot.
                        else if (settingInput != null && bool.TryParse(settingInput, out var setting))
                        {
                            _botReader.StoreBot(bots[option].Key, setting);
                            _logger.Log($"Auto restart setting changed to {setting} for bot {bots[option].Key}", ConsoleColor.Gray, false);
                            hasCorrectSetting = true;
                        }
                        else _logger.Log("That's not a correct option.", ConsoleColor.Gray, false);
                    }
                }
                else _logger.Log("That's not a correct option.", ConsoleColor.Gray, false);
            }
        }
    }
}
