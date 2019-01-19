using System.Collections.Generic;

namespace BotRestarter.Interfaces
{
    public interface IBotReader
    {


        /// <summary>
        /// Loads all the bot file paths in the bots folder.
        /// </summary>
        /// <returns>
        /// A array of strings containing the file paths.
        /// </returns>
        string[] GetBotFiles();


        /// <summary>
        /// Stores a bot from the ConcurrentDictionary.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value of the you are trying to find. This should be the filename or the filepath of the bot.</param>
        /// <param name="shouldRestart">The <see cref="bool"/> if the bot should keep restarting when it closes.</param>
        void StoreBot(string key, bool shouldRestart);


        /// <summary>
        /// Gets a <see cref="bool"/> value whether a bot should keep restarting.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value of the bot you are trying to find. This should be the filename or the filepath of the bot.</param>
        /// <returns>
        /// A <see cref="bool"/> value whether a bot should keep restarting.
        /// </returns>
        bool GetShouldRestartBot(string key);


        /// <summary>
        /// Removes a bot from the ConcurrentDictionary.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value of the bot you are trying to find. This should be the filename or the filepath of the bot.</param>
        /// <returns>
        /// A <see cref="bool"/> value whether a bot should keep restarting.
        /// </returns>
        void RemoveBot(string key);


        /// <summary>
        /// Gets all the bot in the ConcurrentDictionary.
        /// </summary>
        /// <returns>
        /// A list of <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </returns>
        List<KeyValuePair<string, bool>> GetAlBots();

    }
}
