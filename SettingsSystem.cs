using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsSystem : MonoBehaviour
{
    [Header("В игре")]
    // Текстовые обьекты из игры
    [SerializeField] private InGameItems InGameItems;
    // Музыка из игры
    [SerializeField] private AudioSource AudioSource;

    [Header("Меню")]
    // Текстовые обьекты из меню
    [SerializeField] private MenuItems menuItems;
    //Музыка из меню
    [SerializeField] private AudioSource music;
    // выбор языка
    [SerializeField] private TMP_Dropdown languages;
    // настройка громкости музыки
    [SerializeField] private Slider volumeMusic;

    [Header("Общее")]
    [SerializeField] private SettingsSystem settings;

    //громкость, когда игрок сдвинул ползунок
    private float newVolume;
    // язык, который игрок выбрал
    private string newLanguage;
    // активная на данный момент сцена
    private Scene scene;

    //язык на данный момент
    private static Language lang;
    //полученные файлы локализации
    private static List<Language> loadLanguages;

    private void Awake()
    {
        InsertingNewValues();
    }
    public void InsertingNewValues()
    {
        scene = SceneManager.GetActiveScene();
        var loadSettings = SaveLoadSystem.LoadSettings();

        if (scene.name == "Menu")
        {
            loadLanguages = SaveLoadSystem.LoadLanguages();
            if (loadLanguages != null)
            {
                languages.options.Clear();
                // добавляем в список для выбора языка, язык, загруженный из файла
                // далее находим в этом списке тот язык, который выбран в файле настроек
                for (int i = 0; i < loadLanguages.Count; i++)
                {
                    languages.options.Add(
                        new TMP_Dropdown.OptionData(loadLanguages[i].language));

                        languages.value = 
                        languages.options[i].text == 
                        loadSettings.language ? i : 0;
                }
                //устанавлвиаем громкость
                settings.volumeMusic.value = loadSettings.volume;

                //заменяем язык на новый
                menuItems.NewValueLang(SaveLoadSystem.LoadLanguage(
                    loadSettings.language));

                // передаем вновь установленные значения в "буфера"
                newLanguage = languages.options[languages.value].text;
                newVolume = settings.volumeMusic.value;
            }
        }
        //если игрок находится на сцене уровня
        else
        {
            AudioSource.volume = loadSettings.volume;

            InGameItems.NewValueLang(SaveLoadSystem.LoadLanguage(
                loadSettings.language));
        }
    }
    //если игрок поменял какую-либо настройку, то сохраняем их
    public void UpdateSettings()
    {
        if (newLanguage != languages.options[languages.value].text
            || newVolume != settings.volumeMusic.value)
        {
            for (int i = 0; i < loadLanguages.Count; i++)
            {
                    lang = loadLanguages[i].language
                           == languages.options[languages.value].text ? 
                           loadLanguages[i] : 
                           null;
            } 
            newLanguage = languages.options[languages.value].text;
            newVolume = settings.volumeMusic.value;

            menuItems.NewValueLang(lang);

            var set = new Setting(volumeMusic.value,
                  languages.options[languages.value].text);
            var js = JsonUtility.ToJson(set, true);

            File.Delete(Application.persistentDataPath + "\\settings.set");

            SaveLoadSystem.SaveMainData(js, "", true);

        }
    }
    private void Update()
    {
        if (scene.name == "Menu")
        {
            music.volume = volumeMusic.value;
            UpdateSettings();
        }
    }
}


