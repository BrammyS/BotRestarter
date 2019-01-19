using System.Threading.Tasks;
using BotRestarter.Interfaces;

namespace BotRestarter
{
    public static class Program
    {

        private static async Task Main()
        {
            Unity.RegisterTypes();
            var botRestarter = Unity.Resolve<IBotRestarter>();
            await botRestarter.StartAsync().ConfigureAwait(false);
        }
    }
}
