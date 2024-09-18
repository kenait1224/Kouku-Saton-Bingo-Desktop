using System.Runtime.InteropServices;
using System.Text;

/// <summary> (2021-04-16)
/// 
/// implement read and write functionality for INI files
/// 
/// </summary>

public class IniFileReader
{
    [DllImport("kernel32",CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    private string filepath;
    public IniFileReader(string filepath)
    {
        this.filepath = filepath;
    }

    public void WriteIni(string section, string key, string val)
    {
        WritePrivateProfileString(section, key, val, filepath);
    }

    public string ReadIni(string section, string key)
    {
        StringBuilder temp = new StringBuilder(255);
        GetPrivateProfileString(section, key, "-1", temp, 255, filepath);
        return temp.ToString().Split(';')[0];
    }

    public string ReadIni(string section, string def ,string key )
    {
        StringBuilder temp = new StringBuilder(255);
        GetPrivateProfileString(section, key, def, temp, 255, filepath);
        return temp.ToString().Split(';')[0];
    }

}
