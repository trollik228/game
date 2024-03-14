using UnityEngine;

// ������� ������
public class ErrorSystem : MonoBehaviour
{
    [SerializeField] private GameObject errorMessage; // ������ ��� ����������� ������

    private static GameObject errorPanel;
    private void Start() { errorPanel = errorMessage; }

    /// <summary>
    /// ���������� �������� ������
    /// </summary>
    /// <param name="message"></param>
    public void ErrorMessage(string message)
    {
        errorPanel.transform.Find("message").GetComponent<TMPro.TMP_Text>().text = message;
        errorPanel.SetActive(true);
        if (errorPanel.activeInHierarchy)
        Invoke(nameof(OffErrorMessage), 2);
    } // �������� ���������, ������� ����� �������� � ������ ������ ���� ������
    public void OffErrorMessage() { errorPanel.SetActive(false); } // ���������� ������
}
