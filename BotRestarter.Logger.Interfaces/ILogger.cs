using System;

namespace DiscordBotRestarter.Logger.Interfaces
{
    public interface ILogger
    {


        /// <summary>
        /// Logs a message to the console.
        /// The <paramref name="color"/> is by default <see cref="ConsoleColor.Gray"/>
        /// </summary>
        /// <param name="message">The message that will be printed to the console.</param>
        /// <param name="color">The color that the message will have when printing something to the console.</param>
        void Log(string message, ConsoleColor color = ConsoleColor.Gray);
    }
}
