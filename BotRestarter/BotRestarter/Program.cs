using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotRestarter
{
    public class Program
    {
        private static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            while (true)
            {
                try
                {
                    Process.Start("ColorChan.lnk");
                    await Task.Delay(1797500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
