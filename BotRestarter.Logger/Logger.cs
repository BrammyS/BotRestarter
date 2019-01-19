using System;
using BotRestarter.Logger.Interfaces;

namespace BotRestarter.Logger
{
    public class Logger : ILogger
    {


        /// <inheritdoc />
        public void Log(string message, ConsoleColor color = ConsoleColor.DarkGray, bool hasTimestamp = true)
        {
            Console.ForegroundColor = color;
            if(hasTimestamp) Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} : " + message);
            else Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
