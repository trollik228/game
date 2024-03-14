using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public static void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (!panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                panel.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                panel.SetActive(false);
        }
    }
}
