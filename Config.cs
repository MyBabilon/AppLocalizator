using Newtonsoft.Json;
using OftobTech.AppLocalizator.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace OftobTech.AppLocalizator
{
    public class Config
    {
        private static ConfigModel _config = null;
        public static string app_path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        /// <summary>
        ///  Publishes the configuration file to the file system
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void PublishConfig()
        {
            if (Directory.Exists(app_path + "\\" + ConfigResource.config_directory_name))
            {
                if (File.Exists(ConfigResource.config_path))
                {
                    throw new Exception("Lang config file is already exists, please delete them and run this command one more time");
                }
            }
            else
            {
                Directory.CreateDirectory(app_path + "\\" + ConfigResource.config_directory_name);
            }

            var stream = File.Create(app_path + "\\" + ConfigResource.config_path);

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

            Console.WriteLine("path to config: " + app_path + "\\" + ConfigResource.config_path);

            if (!File.Exists(app_path + "\\" + ConfigResource.config_path))
            {
                
                configData = Parser.ParseFile(app_path + "\\" + ConfigResource.config_path);
                if(configData == null)
                {
                    throw new Exception("Read configuration error");
                }
            }
            else
            {
                var fileContent = File.ReadAllText(app_path + "\\" + ConfigResource.config_path);

                configData = Parser.ParseText(fileContent);
            }

            config.DefaultLang = tryGetValue(configData, "DefaultLang", "en");
            config.LangsFilesPath = tryGetValue(configData, "LangsFilesPath", @"Langs");

            return config;
        }

        public static void UpdateConfig()
        {
            _config = ReadConfig();
        } 

        public static ConfigModel getConfig()
        {
            return _config;
        }


        #pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
        private static string tryGetValue(Dictionary<string, string> dict, string key, string default_value)
        {
            if(dict.TryGetValue(key, out var value)){
                return value;
            }
               
            return default_value;
        }
        #pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.
    }
}
