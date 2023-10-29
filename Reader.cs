using System.Reflection;
using System.Collections.Generic;
using MyBabilon.Models;

namespace MyBabilon.FilesReader
{
    public class Reader
    {
        protected static void UpdateLangs(bool forece = false)
        {
            if (LangModel.Languages == null || forece)
            {
                LangModel.Languages = scanAppFolder();
            }
        }

        protected static Dictionary<string, Dictionary<string, string>> scanAppFolder()
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            path = path + @"\Langs";

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var  langs = new Dictionary<string, Dictionary<string, string>>();

            foreach (string file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                var parts = file.Split('\\');
                var fileName = (parts.Length > 0) ? parts[parts.Length - 1] : null;

                if (fileName == null) continue;

                var langParts = fileName.Split('.');
                var lang = (langParts.Length == 2)? langParts[0] : null;

                if (fileName == null) continue;

                langs[lang] = MbdfReader.Parser.ParseFile(file);
            }

            return langs;
        }
    }
}