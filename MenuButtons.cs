using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    //панель для кнопок
    [SerializeField] private GameObject levelsPanel;
    //панель для настроек
    [SerializeField] private GameObject settingsPanel;
    //префаб копки для загрузки уровня
    [SerializeField] private GameObject prefabButton;
    //панель с создателями
    [SerializeField] private GameObject credits;

    private List<GameObject> buttons = new();

    private bool isOpenLevelPanel = false;
    private bool isLoadLevelsInfo = false;


    public void Play()
    {
        isOpenLevelPanel = !isOpenLevelPanel;

        if (isOpenLevelPanel)
        {
            levelsPanel.SetActive(true);

            if (!isLoadLevelsInfo)
            {
                var loadlLevels = SaveLoadSystem.LoadLevels();
                for (int i = 0; i < loadlLevels.Length; i++)
                {
                    if (loadlLevels.Length > buttons.Count) 
                        buttons.Add(prefabButton);

                    GameObject obj = Instantiate(prefabButton);
                    obj.transform.SetParent(levelsPanel.transform.Find("LevelsPanel"));

                    if (loadlLevels[i].nameLevel != 
                        buttons[i].GetComponentInChildren<TMPro.TMP_Text>().text 
                        && obj != null)
                        obj.GetComponentInChildren<TMPro.TMP_Text>().text =
                                                    loadlLevels[i].nameLevel;
                }

                isLoadLevelsInfo = true;
            }
        }
        else levelsPanel.SetActive(false);

    }
    public void Settings() { settingsPanel.SetActive(true); }
    public void Creators() { credits.SetActive(true); }
    public void Exit() { Application.Quit(); }

    public void PlayLevel(Button isp)
    {
        SaveLoadSystem.levelName = isp.GetComponentInChildren<TMPro.TMP_Text>().text;
        SceneManager.LoadScene("Level");

    }

    private void Update()
    {
        if ((levelsPanel.activeSelf) || (settingsPanel.activeSelf) || 
            (credits.activeSelf))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                levelsPanel.SetActive(false);
                settingsPanel.SetActive(false);
                credits.SetActive(false);
            }
        }
    }
}
