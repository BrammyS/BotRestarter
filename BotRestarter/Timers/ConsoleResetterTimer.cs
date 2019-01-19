using System;
using System.Threading.Tasks;
using System.Timers;
using BotRestarter.Logger.Interfaces;

namespace BotRestarter.Timers
{
    public class ConsoleReSetterTimer
    {
        private readonly ILogger _logger;

        public ConsoleReSetterTimer(ILogger logger)
        {
            _logger = logger;
        }

        private Timer _timer;
        internal Task TimerAsync()
        {
            _timer = new Timer
            {
                Interval = TimeSpan.FromDays(1).TotalMilliseconds,
                AutoReset = true,
                Enabled = true
            };
            _timer.Elapsed += TimerElapsed;
            return Task.CompletedTask;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Log("Clearing console...", ConsoleColor.Red);
            Console.Clear();
            _logger.Log("Console cleared", ConsoleColor.Green);
        }
    }
}
