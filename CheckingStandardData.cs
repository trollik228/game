using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CheckingStandardData : MonoBehaviour
{
    // ����������� ���������� �� ����� ������
    [SerializeField] private Level level;
    // ����������� ���� ��� ���� � ����� ����
    [SerializeField] private InGame gameLanguage;
    [SerializeField] private Menu menuItems;

    //����������� ���������(��-�� ������)
    private readonly string STANDART_SETTINGS = 
        "{\r\n    \"volume\": 1.0,\r\n    \"language\": \"Russian\"\r\n}";


    private void Awake()
    {
        // �������� ������������(��) �������
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "\\Levels"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath
                                                + "\\Levels");

            SaveLoadSystem.SaveLevel(level,1);
        }
        // �������� ������������(��) �����(��) � ������������
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
        //�������� ������������ ����� � �����������
        if (!System.IO.File.Exists(Application.persistentDataPath + "\\settings.set"))
        {
            SaveLoadSystem.SaveMainData(STANDART_SETTINGS, "set", true);
        }
    }
}
