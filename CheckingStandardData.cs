using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CheckingStandardData : MonoBehaviour
{
    // стандартная информация об одном уровне
    [SerializeField] private Level level;
    // стандартный язык для меню и самой игры
    [SerializeField] private InGame gameLanguage;
    [SerializeField] private Menu menuItems;

    //стандартные настрйоки(да-да строка)
    private readonly string STANDART_SETTINGS = 
        "{\r\n    \"volume\": 1.0,\r\n    \"language\": \"Russian\"\r\n}";


    private void Awake()
    {
        // Создание стандартного(ых) уровней
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "\\Levels"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath
                                                + "\\Levels");

            SaveLoadSystem.SaveLevel(level,1);
        }
        // создание стандартного(ых) файла(ов) с локализацией
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "\\Languages"))
        {
                System.IO.Directory.CreateDirectory(Application.persistentDataPath
                                                    + "\\Languages");

            SaveLoadSystem.SaveMainData(
                JsonUtility.ToJson(new Language(menuItems, gameLanguage, "Russian"), true),
                "lang",
                false,
                "Russian");
        }
        //Создание стандартного файла с настройками
        if (!System.IO.File.Exists(Application.persistentDataPath + "\\settings.set"))
        {
            SaveLoadSystem.SaveMainData(STANDART_SETTINGS, "set", true);
        }
    }
}
