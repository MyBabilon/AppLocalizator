
/* Необъединенное слияние из проекта "OftobTech.AppLocalizator (net5.0)"
До:
using System.Reflection;
using System.Collections.Generic;
using OftobTech.AppLocalizator.Models;
using OftobTech.AppLocalizator;
После:
using System.AppLocalizator;
using OftobTech.AppLocalizator.Models;
using System;
using System.Collections.Generic;
*/
using OftobTech.AppLocalizator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OftobTech.AppLocalizator
{
    public class Reader
    {
        /// <summary>
        /// Updating the language files in the application memory
        /// </summary>
        /// <param name="force">Forced launch, overwriting existing data</param>
        public static void UpdateLangs(bool forсе = false)
        {
            if (LangModel.Languages == null || forсе)
            {
                if (LangModel.Languages != null) LangModel.Languages.Clear();
                LangModel.Languages = ScanAppFolder();
            }
        }

        /// <summary>
        /// Scaning Application lang folder
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Dictionary<string, Dictionary<string, string>>? ScanAppFolder()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                throw new Exception("Critical error cant get Assembly");
            }

            var path = Path.GetDirectoryName(assembly.Location);

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
                var lang = (langParts.Length == 2) ? langParts[0] : "";

                if (fileName == null) continue;

                langs[lang] = new Dictionary<string, string>(Parser.ParseFile(file));
            }
            return langs;
        }
    }
}