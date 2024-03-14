using UnityEngine;

// Класс для предотвращения постройки башни в башни
public class StartPrefabLogic : MonoBehaviour
{
    private  bool isTowerTrigger = false;
    private bool isPlaneBuildTrigger = false;

    private int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            i++;
            isTowerTrigger = true;
            Building.allowConstruction = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if (other.CompareTag("Plane For Building"))
        {
            isPlaneBuildTrigger = true;

            if (!isTowerTrigger)
            {
                Building.allowConstruction = true;
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            i--;
            if (i <= 0)
            {
                if (isPlaneBuildTrigger)
                {
                    isTowerTrigger = false;
                    gameObject.GetComponent<Renderer>().material.color = Color.green;
                    Building.allowConstruction = true;
                }
                else
                    isTowerTrigger = false;
            }
        }
        if (other.CompareTag("Plane For Building"))
        {
            isPlaneBuildTrigger = false;
            Building.allowConstruction = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
