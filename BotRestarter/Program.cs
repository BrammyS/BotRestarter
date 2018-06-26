using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotRestarter
{
    public class Program
    {
        private string[] filePaths;
        private static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            while (true)
            {
                try
                {
                    GetFiles();
                    StartPrograms();
                    await Task.Delay(1797500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        private void GetFiles()
        {
            filePaths = Directory.GetFiles("bots", "*.lnk", SearchOption.AllDirectories);
            foreach (var file in filePaths)
            {
                Console.WriteLine(file);
            }
        }

        private void StartPrograms()
        {
            foreach (var file in filePaths)
            {
                Console.WriteLine($"{DateTime.Now:G} : Starting {file}");
                Process.Start(file);
            }
        }
    }
}
