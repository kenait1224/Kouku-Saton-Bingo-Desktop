using Kouku_Saton_Bingo.Model;

namespace Kouku_Saton_Bingo
{
    public class Parameter
    {
        string iniFilePath;

        // Hot Key
        //public string strClear_HotKey = "Alt + Q";
        //public string strUndo_HotKey = "Alt + E";

        //Display
        public SettingsModelInt iFormSize;
        public SettingsModelInt iOpacity;
        public SettingsModelInt iLangauge;
        public SettingsModelBoolean bRecommendedIsShow;
        public SettingsModelPoint pPosition;

        public Parameter(string path)
        {
            this.iniFilePath = path + @"\config.ini";
            LoadConfig();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void LoadConfig()
        {
            IniFileReader IniFile = new IniFileReader(iniFilePath);

            //this.strClear_HotKey = IniFile.ReadIni("Hot Key", "Alt + Q", "Clear HotKey");
            //this.strUndo_HotKey = IniFile.ReadIni("Hot Key", "Alt + E", "Undo HotKey");

            this.iFormSize = new SettingsModelInt(IniFile.ReadIni("Display", "300", " Form Size"));
            this.iOpacity = new SettingsModelInt(IniFile.ReadIni("Display", "85", "Opacity"));
            this.iLangauge = new SettingsModelInt(IniFile.ReadIni("Display", "0", "Langauge"));
            this.pPosition = new SettingsModelPoint(IniFile.ReadIni("Display", "200,200", "Position"));
            this.bRecommendedIsShow = new SettingsModelBoolean(IniFile.ReadIni("Display", "False", "Recommended"));


        }

        public void SaveConfig()
        {
            IniFileReader IniFile = new IniFileReader(iniFilePath);

            //IniFile.WriteIni("Hot Key", "Clear HotKey", this.strClear_HotKey.ToString() + ";");
            //IniFile.WriteIni("Hot Key", "Undo HotKey", this.strUndo_HotKey.ToString() + ";");

            IniFile.WriteIni("Display", "Form Size", this.iFormSize.ToString() + ";");
            IniFile.WriteIni("Display", "Opacity", this.iOpacity.ToString() + ";");
            IniFile.WriteIni("Display", "Langauge", this.iLangauge.ToString() + ";");
            IniFile.WriteIni("Display", "Position", this.pPosition.ToString() + ";");
            IniFile.WriteIni("Display", "Recommended", this.bRecommendedIsShow.ToString() + ";");
        }
    }
}
