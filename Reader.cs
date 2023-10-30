using System.Reflection;
using System.Collections.Generic;
using OftobTech.AppLocalizator.Models;
using OftobTech.AppLocalizator;
using System.IO;
using System;

namespace OftobTech.AppLocalizator
{
    public class Reader
    {
        public static void UpdateLangs(bool forece = false)
        {
            if (LangModel.Languages == null || forece)
            {
                if(LangModel.Languages != null) LangModel.Languages.Clear();
                LangModel.Languages = scanAppFolder();
            }
        }

        public static Dictionary<string, Dictionary<string, string>>? scanAppFolder()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var config = Config.getConfig();

            path = path + "\\" + config.LangsFilesPath;
            Console.WriteLine("Langs files path: " + path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Dictionary<string, Dictionary<string, string>>? langs = new Dictionary<string, Dictionary<string, string>>();

            foreach (string file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                var parts = file.Split('\\');
                var fileName = (parts.Length > 0) ? parts[parts.Length - 1] : null;

                if (fileName == null) continue;

                var langParts = fileName.Split('.');
                var lang = (langParts.Length == 2)? langParts[0] : null;

                if (fileName == null) continue;

                langs[lang] = new Dictionary<string, string>( Parser.ParseFile(file));
            }
            return langs;
        }
    }
}