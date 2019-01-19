using System;
using BotRestarter.Logger.Interfaces;

namespace BotRestarter.Logger
{
    public class Logger : ILogger
    {


        /// <inheritdoc />
        public void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} : " + message);
            Console.ResetColor();
        }
    }
}
