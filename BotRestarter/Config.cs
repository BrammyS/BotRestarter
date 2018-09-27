using System.IO;
using Newtonsoft.Json;

namespace BotRestarter
{
    public class Config
    {
        private const string ConfigFolder = "../Color-chan/Configs";
        private const string ConfigFile = "ConfigData.json";

        public static RestartConfig ConfigData;

        static Config()
        {
            if (!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);

            if (!File.Exists(ConfigFolder + "/" + ConfigFile))
            {
                ConfigData = new RestartConfig();
                var json = JsonConvert.SerializeObject(ConfigData, Formatting.Indented);
                File.WriteAllText(ConfigFolder + "/" + ConfigFile, json);
            }
            else
            {
                var json = File.ReadAllText(ConfigFolder + "/" + ConfigFile);
                ConfigData = JsonConvert.DeserializeObject<RestartConfig>(json);
            }
        }
    }

    public struct RestartConfig
    {
        public string Token;
        public string CmdPrefix;
        public ulong Id;
        public string ApiToken;
        public int RestartTime;
    }
}