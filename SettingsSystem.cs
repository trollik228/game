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
    [Header("� ����")]
    // ��������� ������� �� ����
    [SerializeField] private InGameItems InGameItems;
    // ������ �� ����
    [SerializeField] private AudioSource AudioSource;

    [Header("����")]
    // ��������� ������� �� ����
    [SerializeField] private MenuItems menuItems;
    //������ �� ����
    [SerializeField] private AudioSource music;
    // ����� �����
    [SerializeField] private TMP_Dropdown languages;
    // ��������� ��������� ������
    [SerializeField] private Slider volumeMusic;

    [Header("�����")]
    [SerializeField] private SettingsSystem settings;

    //���������, ����� ����� ������� ��������
    private float newVolume;
    // ����, ������� ����� ������
    private string newLanguage;
    // �������� �� ������ ������ �����
    private Scene scene;

    //���� �� ������ ������
    private static Language lang;
    //���������� ����� �����������
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
                // ��������� � ������ ��� ������ �����, ����, ����������� �� �����
                // ����� ������� � ���� ������ ��� ����, ������� ������ � ����� ��������
                for (int i = 0; i < loadLanguages.Count; i++)
                {
                    languages.options.Add(
                        new TMP_Dropdown.OptionData(loadLanguages[i].language));

                        languages.value = 
                        languages.options[i].text == 
                        loadSettings.language ? i : 0;
                }
                //������������� ���������
                settings.volumeMusic.value = loadSettings.volume;

                //�������� ���� �� �����
                menuItems.NewValueLang(SaveLoadSystem.LoadLanguage(
                    loadSettings.language));

                // �������� ����� ������������� �������� � "������"
                newLanguage = languages.options[languages.value].text;
                newVolume = settings.volumeMusic.value;
            }
        }
        //���� ����� ��������� �� ����� ������
        else
        {
            AudioSource.volume = loadSettings.volume;

            InGameItems.NewValueLang(SaveLoadSystem.LoadLanguage(
                loadSettings.language));
        }
    }
    //���� ����� ������� �����-���� ���������, �� ��������� ��
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


