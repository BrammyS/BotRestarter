using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BotRestarter.Interfaces;
using BotRestarter.Logger.Interfaces;

namespace BotRestarter
{
    public class BotReader : IBotReader
    {
        private readonly ILogger _logger;
        private static readonly ConcurrentDictionary<string, bool> Bots = new ConcurrentDictionary<string, bool>();


        /// <summary>
        /// Creates a new <see cref="BotReader"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that will be used to log messages to the console.</param>
        public BotReader(ILogger logger)
        {
            _logger = logger;
        }


        /// <inheritdoc />
        public string[] GetBotFiles()
        {

            // If Bots directory doesn't exist, create one.
            if (!Directory.Exists("Bots")) Directory.CreateDirectory("Bots");

            // Load all the bot shortcuts in the Bots folder.
            var botFilePaths = Directory.GetFiles("bots", "*.lnk", SearchOption.AllDirectories);
            foreach (var file in botFilePaths) _logger.Log($"Found {file}!");
            return botFilePaths;
        }


        /// <inheritdoc />
        public void StoreBot(string key, bool shouldRestart)
        {
            if (Bots.ContainsKey(key))
            {
                var oldValue = GetShouldRestartBot(key);
                if (!Bots.TryUpdate(key, shouldRestart, oldValue)) _logger.Log($"Failed to update ShouldRestart value with the key: {key} from the dictionary", ConsoleColor.Red);
                return;
            }

            if(!Bots.TryAdd(key, shouldRestart)) _logger.Log($"Failed to add ShouldRestart value with the key: {key} from the dictionary", ConsoleColor.Red);
        }


        /// <inheritdoc />
        public List<KeyValuePair<string, bool>> GetAlBots()
        {
            return Bots.ToList();
        }

        /// <inheritdoc />
        public bool GetShouldRestartBot(string key)
        {
            return Bots.ContainsKey(key) && Bots[key];
        }


        /// <inheritdoc />
        public void RemoveBot(string key)
        {
            if (!Bots.ContainsKey(key)) return;
            if (!Bots.TryRemove(key, out _)) _logger.Log($"Failed to remove bot with the key: {key} from the dictionary", ConsoleColor.Red);
        }
    }
}
