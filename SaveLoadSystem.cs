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
    ///  Сохраняет Level (второй аргумент + level = имя файла)
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
    /// Сохраняет JSONстроку в файл по встроенному индексу(indexLevelFile)
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
    /// Предназначен для создания стандарт значений
    /// (еще несозданные). Например:
    /// для файлов локализации и файла настроек
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
    /// Возвращает данные для уровня(Level)
    /// </summary>
    /// <returns></returns>
    public static Level LoadLevel()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + 
                                            "\\Levels\\", "*.lvl");
        Level[] infoLevels = new Level[files.Length];

        Debug.Log("Найдено файлов уровней – " + files.Length);

        for (int i = 0;  i < files.Length; i++)
                infoLevels[i] = JsonUtility.FromJson<Level>
                (Cryptography.FileDecryption(files[i],true));

        return FindLevel(infoLevels);
    }

    /// <summary>
    /// Возвращает информацию из файлов с локализацией
    /// </summary>
    /// <returns></returns>
    public static List<Language> LoadLanguages()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + 
                                            "\\Languages\\", "*.lang");
        List<Language> infoLevels = new();

        Debug.Log("Найдено файлов с локализацией – " + files.Length);

        for (int i = 0; i < files.Length; i++)
            infoLevels.Add(JsonUtility.FromJson<Language>(ReturnJsonStringFromFile(files[i])));

        return infoLevels;
    }

    /// <summary>
    /// Возвращает информацию из одного файла с локализацией
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
    /// Возвращает информацию из файла с настройками
    /// </summary>
    /// <returns></returns>
    public static Setting LoadSettings()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.set");

        Debug.Log("Найдено файлов с настройками – " + files.Length);

        Setting infoLevels;
        infoLevels = JsonUtility.FromJson<Setting>(ReturnJsonStringFromFile(files[0]));

        return infoLevels;
    }

    // Вовзращает строку с информацией из файла
    // (скажем так - метод для всего)
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
    /// Шифровка файла
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
    /// Возвращает массив с найденными levels
    /// </summary>
    /// <returns></returns>
    public static Level[] LoadLevels()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + 
                                            "\\Levels", "*.lvl");
        Level[] infoLevels = new Level[files.Length];
        Debug.Log("Найдено файлов – " + files.Length);

        for (int i = 0; i < files.Length; i++)
                infoLevels[i] = JsonUtility.FromJson<Level>
                (Cryptography.FileDecryption(files[i],true)); 
        
        return infoLevels;
    }

    // Возвращает информацию о уровне из файла
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
