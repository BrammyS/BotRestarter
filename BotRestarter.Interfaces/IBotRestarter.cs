using System.Threading.Tasks;

namespace BotRestarter.Interfaces
{
    public interface IBotRestarter
    {


        /// <summary>
        /// Starts all the methods to start all the bots and all the methods to keep them alive.
        /// </summary>
        Task StartAsync();
    }
}
