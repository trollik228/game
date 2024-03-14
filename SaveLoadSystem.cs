using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class SaveLoadSystem : MonoBehaviour
{
    public static string levelName;
    public static int indexLevelFile = 1;

    #region Save_Data
    /// <summary>
    ///  ��������� Level (������ �������� + level = ��� �����)
    /// </summary>
    /// <param name="infoLevel"></param>
    public static void SaveLevel(Level infoLevel, int indexLevel)
    {
        var js = JsonUtility.ToJson(infoLevel, true);
        using (FileStream fstream = new(Application.persistentDataPath
                                        + $"\\Levels\\{indexLevel}level.lvl",
                                        FileMode.Open))
        {
            byte[] buffer = Encoding.Default.GetBytes(js);
             fstream.Write(buffer, 0, buffer.Length);
        }
        EncryptionLevelFile($"{indexLevel}level.lvl");
    }

    /// <summary>
    /// ��������� JSON������ � ���� �� ����������� �������(indexLevelFile)
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static string SaveJsonString(string jsonString)
    {
        using (FileStream fstream = new(Application.persistentDataPath
                                        + $"\\Levels\\{indexLevelFile}level.lvl", 
                                        FileMode.Open))
        {
            byte[] buffer = Encoding.Default.GetBytes(jsonString);
            fstream.Write(buffer, 0, buffer.Length);
        }
        indexLevelFile++;
        return "";
    }

    /// <summary>
    /// ������������ ��� �������� �������� ��������
    /// (��� �����������). ��������:
    /// ��� ������ ����������� � ����� ��������
    /// </summary>
    /// <param name="jsonString"></param>
    /// <param name="expand"></param>
    /// <param name="isSettings"></param>
    /// <param name="standartNameFileLanguage"></param>
    /// <returns></returns>
    public static void SaveMainData(string jsonString,string expand,
                                  bool isSettings = false, 
                                  string standartNameFileLanguage = "Russian")
    {
        string path = isSettings ? Application.persistentDataPath + "\\settings.set" : 
                                   Application.persistentDataPath + 
                                   $"\\Languages\\{standartNameFileLanguage}.{expand}";

        using (FileStream fstream = new(path,
            FileMode.OpenOrCreate))
        {
            byte[] buffer = Encoding.Default.GetBytes(jsonString);
            fstream.Write(buffer, 0, buffer.Length);
        }
    }
    #endregion

    #region Load_Data

    /// <summary>
    /// ���������� ������ ��� ������(Level)
    /// </summary>
    /// <returns></returns>
    public static Level LoadLevel()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + 
                                            "\\Levels\\", "*.lvl");
        Level[] infoLevels = new Level[files.Length];

        Debug.Log("������� ������ ������� � " + files.Length);

        for (int i = 0;  i < files.Length; i++)
                infoLevels[i] = JsonUtility.FromJson<Level>
                (Cryptography.FileDecryption(files[i],true));

        return FindLevel(infoLevels);
    }

    /// <summary>
    /// ���������� ���������� �� ������ � ������������
    /// </summary>
    /// <returns></returns>
    public static List<Language> LoadLanguages()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + 
                                            "\\Languages\\", "*.lang");
        List<Language> infoLevels = new();

        Debug.Log("������� ������ � ������������ � " + files.Length);

        for (int i = 0; i < files.Length; i++)
            infoLevels.Add(JsonUtility.FromJson<Language>(ReturnJsonStringFromFile(files[i])));

        return infoLevels;
    }

    /// <summary>
    /// ���������� ���������� �� ������ ����� � ������������
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    public static Language LoadLanguage(string lang)
    {
        return JsonUtility.FromJson<Language>
                   (ReturnJsonStringFromFile(Application.persistentDataPath + 
                                   $"\\Languages\\{lang}.lang"));
    }

    /// <summary>
    /// ���������� ���������� �� ����� � �����������
    /// </summary>
    /// <returns></returns>
    public static Setting LoadSettings()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.set");

        Debug.Log("������� ������ � ����������� � " + files.Length);

        Setting infoLevels;
        infoLevels = JsonUtility.FromJson<Setting>(ReturnJsonStringFromFile(files[0]));

        return infoLevels;
    }

    // ���������� ������ � ����������� �� �����
    // (������ ��� - ����� ��� �����)
    private static string ReturnJsonStringFromFile(string file)
    {
        string textFromFile;
        using (FileStream fstream = new(file,FileMode.Open))
        {
            byte[] buffer = new byte[fstream.Length];
            fstream.Read(buffer, 0, buffer.Length);
            textFromFile = Encoding.Default.GetString(buffer);
        }
        
        return textFromFile;
    }

    #endregion

    #region Other
    /// <summary>
    /// �������� �����
    /// </summary>
    /// <param name="file"></param>
    public static void EncryptionLevelFile(string file)
    {
        string textFromFile;

        using (FileStream fstream = File.OpenRead(Application.persistentDataPath +
                                                  $"\\Levels\\{file}"))
        {
            byte[] buffer = new byte[fstream.Length];
            fstream.Read(buffer, 0, buffer.Length);
            textFromFile = Encoding.Default.GetString(buffer);  
        }
        Cryptography.FileEncryption(file, textFromFile, false);
    }

    /// <summary>
    /// ���������� ������ � ���������� levels
    /// </summary>
    /// <returns></returns>
    public static Level[] LoadLevels()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + 
                                            "\\Levels", "*.lvl");
        Level[] infoLevels = new Level[files.Length];
        Debug.Log("������� ������ � " + files.Length);

        for (int i = 0; i < files.Length; i++)
                infoLevels[i] = JsonUtility.FromJson<Level>
                (Cryptography.FileDecryption(files[i],true)); 
        
        return infoLevels;
    }

    // ���������� ���������� � ������ �� �����
    private static Level FindLevel(Level[] findFiles)
    {
        Level thisLevel = new();
        for (int i = 0; i < findFiles.Length; i++)
        {
            if (levelName == findFiles[i].nameLevel) 
                thisLevel = findFiles[i];
        }
        return thisLevel;
    }

    #endregion
}
