using UnityEngine;

// Система Ошибок
public class ErrorSystem : MonoBehaviour
{
    [SerializeField] private GameObject errorMessage; // панель для отображения ошибки

    private static GameObject errorPanel;
    private void Start() { errorPanel = errorMessage; }

    /// <summary>
    /// Отображает описание ошибки
    /// </summary>
    /// <param name="message"></param>
    public void ErrorMessage(string message)
    {
        errorPanel.transform.Find("message").GetComponent<TMPro.TMP_Text>().text = message;
        errorPanel.SetActive(true);
        if (errorPanel.activeInHierarchy)
        Invoke(nameof(OffErrorMessage), 2);
    } // передаем сообщение, которое будет выведено в правом нижнем углу экрана
    public void OffErrorMessage() { errorPanel.SetActive(false); } // отключение панели
}
