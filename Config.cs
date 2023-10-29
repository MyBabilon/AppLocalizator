using MyBabilon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBabilon.Config
{
    public static class Config
    {
        /// <summary>
        ///  Publishes the configuration file to the file system
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void PublishConfig()
        {
            if (Directory.Exists(ConfigResource.config_directory_name))
            {
                if (File.Exists(ConfigResource.config_path))
                {
                    throw new Exception("Lang config file is already exists, please delete them and run this command one more time");
                }
            }
            else
            {
                Directory.CreateDirectory(ConfigResource.config_directory_name);
            }

            var stream = File.Create(ConfigResource.config_path);

            stream.Write(Encoding.UTF8.GetBytes(ConfigResource.def_config));
            stream.Close();
        }

        /// <summary>
        /// Reading the config, the config is read from the file system 
        /// if it was created, if not, the default config is taken
        /// </summary>
        /// <returns>Configuration сontent</returns>
        public static ConfigModel ReadConfig()
        {
            var config = new ConfigModel();
            Dictionary<string, string>? configData = null;

            if (!File.Exists(ConfigResource.config_path))
            {
                configData = MbdfReader.Parser.ParseFile(ConfigResource.config_path);
                if(configData == null)
                {
                    throw new Exception("Read configuration error");
                }
            }
            else
            {
                var fileContent = File.ReadAllText(ConfigResource.config_path);

                configData = MbdfReader.Parser.ParseText(fileContent);
            }

            config.DefaultLang = tryGetValue(configData, "DefaultLang", "en");
            config.LangsFilesPath = tryGetValue(configData, "LangsFilesPath", @"App\Langs");

            return config;
        }

        #pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
        private static string tryGetValue(Dictionary<string, string> dict, string key, string default_value)
        {
            return dict
                .Where(e => e.Key == key)
                .Select(e => (KeyValuePair<string, string>?)e)
                .FirstOrDefault(new KeyValuePair<string, string>(key, default_value))
                .Value.Value;
        }
        #pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.
    }
}
