using System;

namespace BotRestarter.Logger.Interfaces
{
    public interface ILogger
    {


        /// <summary>
        /// Logs a message to the console.
        /// The <paramref name="color"/> is by default <see cref="ConsoleColor.DarkGray"/>
        /// </summary>
        /// <param name="message">The message that will be printed to the console.</param>
        /// <param name="color">The color that the message will have when printing something to the console.</param>
        /// <param name="hasTimestamp">Set this to false to disable the timestamp.</param>
        void Log(string message, ConsoleColor color = ConsoleColor.DarkGray, bool hasTimestamp = true);
    }
}
