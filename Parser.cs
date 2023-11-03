using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OftobTech.AppLocalizator
{
    public class Parser
    {
        protected static ILog _log = LogManager.GetLogger(typeof(Parser));
        /// <summary>
        /// Parsing text into strings
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseText(string content)
        {
            return Parse(content.Split(new[] { '\r', '\n' }).ToList());
        }

        /// <summary>
        /// Parsing a file into strings
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseFile(string path)
        {
            try
            {
                var fileLines = ReadFile(path);
                return Parse(fileLines);
            }
            catch (Exception ex)
            {
                _log.Error("ParseFile ERROR: ", ex);
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Parsing the string is divided into keys and values
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Parse(List<string> lines)
        {
            var result = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                var readyLine = line.Trim();
                if (readyLine.StartsWith('#')) continue; // Is comment
                if (readyLine.Length == 0) continue; // Empty string
                var LineParts = readyLine.Split(":");

                if (LineParts.Length > 2)
                {
                    for (var i = 2; i < LineParts.Length; i++)
                    {
                        LineParts[1] += ":";
                        LineParts[1] += LineParts[i];
                    }
                }

                result.Add(LineParts[0].Trim(), LineParts[1].Trim());
            }

            return result;
        }

        /// <summary>
        /// Reading a data file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
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
