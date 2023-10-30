using OftobTech.AppLocalizator.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OftobTech.AppLocalizator
{
    public class T : Reader
    {

        protected string? _lang;

        public string getLang()
        {
            if (this._lang == null)
                throw new Exception("Language not selected, please use setLang method");

            return (string)this._lang;
        }

        public T(string? lang = null)
        {
            if(lang != null)
            {
                this._lang = lang;
            }
            Reader.UpdateLangs();
        }
        
        public static T setLang(string lang)
        {
            var instance = new T(lang.ToLower());
            return instance;
        }

        public string? Compile(string iteredString, bool stricMode = false)
        {
            if (LangModel.Languages.TryGetValue(getLang(), out var Strings))
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

        public string? Compile(string iteredString, Dictionary<string, string> forReplace, bool stricMode = false)
        {
            if (LangModel.Languages.TryGetValue(getLang(), out var Strings))
            {
                if (Strings.TryGetValue(iteredString, out var result))
                {
                    foreach (var data in forReplace)
                    {
                        result.Replace("{" + data.Key + "}", data.Value);
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
