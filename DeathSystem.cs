using UnityEngine;
using UnityEngine.SceneManagement;

// скрипт вешается на "ворота"
public class DeathSystem : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text health;
    [SerializeField] private GameObject panelDeath;

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            health.text = (int.Parse(health.text) - 
                other.GetComponent<Npc>().damage).ToString();
        }
    }

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
        if (int.Parse(health.text) <= 0)
        {
            Time.timeScale = 0;
            panelDeath.SetActive(true);
        }
    }
}
