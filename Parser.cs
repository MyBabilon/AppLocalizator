using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OftobTech.AppLocalizator
{
    public class Parser
    {
        public static Dictionary<string, string> ParseText(string content)
        {
            return Parse(content.Split(new[] { '\r', '\n' }).ToList());
        }
        public static Dictionary<string, string> ParseFile(string path)
        {
            var fileLines = ReadFile(path);
            return Parse(fileLines);
        }

        public static Dictionary<string, string> Parse(List<string> lines)
        {
            var result = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                var readyLine = line.Trim();
                if (readyLine.StartsWith('#')) continue; // Is comment
                if (readyLine.Length == 0) continue; // Empty string
                var lineParts = readyLine.Split(":");

                result.Add(lineParts[0].Trim(), lineParts[1].Trim());
            }

            return result;
        }

        public static List<string> ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }

            var lines = File.ReadLines(path).ToList();

            return lines;
        }
    }
}
