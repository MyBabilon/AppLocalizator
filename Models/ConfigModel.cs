namespace OftobTech.AppLocalizator.Models
{
    public class ConfigModel
    {
        public string? DefaultLang { get; set; }
        public string? LangsFilesPath { get; set; }

        public override string ToString()
        {
            return "DefaultLang: " + this.DefaultLang + ", LangsFilesPath: " + LangsFilesPath;
        }
    }
}
