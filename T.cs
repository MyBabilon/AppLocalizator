using OftobTech.AppLocalizator.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OftobTech.AppLocalizator
{
    public class T : Reader
    {

        protected static string? _lang;
        protected static T? _instance;

        public string getLang()
        {
            if (_lang == null)
                throw new Exception("Language not selected, please use setLang method");

            return (string)_lang;
        }

        public T(string? lang = null)
        {
            if(lang != null)
            {
                _lang = lang;
            }
            else
            {
                _lang = Config._config.DefaultLang;
            }
            Reader.UpdateLangs();

        }
        
        private static T Init()
        {
            if(_instance == null)
            {
                _instance = new T((_lang != null ? _lang: null));
            }
            
            return _instance;
        }

        public static string? Compile(string iteredString, bool stricMode = false)
        {
            var instance = Init();
            if (LangModel.Languages.TryGetValue(instance.getLang(), out var Strings))
            {
                if(Strings.TryGetValue(iteredString, out var result))
                {
                    return result;
                }

                if (result == null && !stricMode) return iteredString;
                else return null;
            }

            if (!stricMode) return iteredString;
            else return null;
        }

        public static string? Compile(string iteredString, Dictionary<string, string> forReplace, bool stricMode = false)
        {
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

        public static List<string> getLangs()
        {
            if(LangModel.Languages != null)
                return LangModel.Languages.Keys.ToList();

            return new List<string>();
        }

    }
}
