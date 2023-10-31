using OftobTech.AppLocalizator.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OftobTech.AppLocalizator
{
    public class T : Reader
    {

        /// <summary> Current language variable</summary>
        protected static string? _lang;
        /// <summary> Instance of this class</summary>
        protected static T? _instance;

        /// <summary>
        /// Retview current language
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getLang()
        {
            if (_lang == null)
                throw new Exception("Language not selected, please use setLang method");

            return (string)_lang;
        }

        /// <summary>
        /// Setting language
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string setLang(string lang)
        {
            lang = PrepareLang(lang);
            return lang;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="lang"></param>
        public T(string? lang = null)
        {
            if (lang != null)
            {
                _lang = lang;
            }
            else
            {
                _lang = Config._config.DefaultLang;
            }
            UpdateLangs();

        }

        /// <summary>
        /// Init new instance of class
        /// </summary>
        /// <returns></returns>
        private static T Init()
        {
            if (_instance == null)
            {
                _instance = new T((_lang != null ? _lang : null));
            }

            return _instance;
        }

        /// <summary>
        /// String Compilation
        /// </summary>
        /// <param name="iteredString">Icoming string</param>
        /// <param name="stricMode">Is strict mode</param>
        /// <returns>Compiled string</returns>
        /// <exception cref="Exception"></exception>
        public static string? Compile(string iteredString, bool stricMode = false)
        {
            if (LangModel.Languages == null)
            {
                throw new Exception("Languages now found");
            }

            var instance = Init();

            if (LangModel.Languages.TryGetValue(instance.getLang(), out var Strings))
            {
                if (Strings.TryGetValue(iteredString, out var result))
                {
                    return result;
                }

                if (result == null && !stricMode) return iteredString;
                else return null;
            }

            if (!stricMode) return iteredString;
            else return null;
        }

        /// <summary>
        /// String Compilation, with additional parametrs 
        /// </summary>
        /// <param name="iteredString"></param>
        /// <param name="forReplace"></param>
        /// <param name="stricMode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string? Compile(string iteredString, Dictionary<string, string> forReplace, bool stricMode = false)
        {
            if (LangModel.Languages == null)
            {
                throw new Exception("Languages now found");
            }
            var instance = Init();

            if (LangModel.Languages.TryGetValue(instance.getLang(), out var Strings))
            {
                if (Strings.TryGetValue(iteredString, out var result))
                {
                    foreach (var data in forReplace)
                    {
                        result = result.Replace("{" + data.Key + "}", data.Value);
                    }
                    return result;
                }

                if (result == null && !stricMode) return iteredString;
                else return null;
            }

            if (!stricMode) return iteredString;
            else return null;
        }

        /// <summary>
        /// Getting a list of available languages
        /// </summary>
        /// <returns></returns>
        public static List<string> getLangs()
        {
            if (LangModel.Languages != null)
                return LangModel.Languages.Keys.ToList();

            return new List<string>();
        }

        /// <summary>
        /// Checking whether there is such a language in the available
        /// </summary>
        /// <param name="lang">Language to check</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsHasLang(string lang)
        {
            if (LangModel.Languages == null)
            {
                throw new Exception("Languages now found");
            }

            if (LangModel.Languages.ContainsKey(lang)) return true;
            return false;
        }

        /// <summary>
        /// Language preparation and checking for availability
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        private static string PrepareLang(string lang)
        {
            lang = lang.ToLower();

            IsHasLang(lang);

            return lang;
        }

    }
}
